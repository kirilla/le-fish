using Lefish.Application.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text;

namespace Lefish.Web.Pages;

[IgnoreAntiforgeryToken]
[AllowAnonymous]
public class ScriptPageModel(
    IUserToken userToken,
    IDatabaseService database,
    IOptions<TemplateConfiguration> templateConfiguration) : UserTokenPageModel(userToken)
{
    private readonly TemplateConfiguration _config = templateConfiguration.Value;

    public EmailTarget EmailTarget { get; set; }
    public Attack Attack { get; set; }
    public PayloadScript PayloadScript { get; set; }

    public async Task<IActionResult> OnGetAsync(int key)
    {
        try
        {
            Attack = await database.Attacks
                .AsNoTracking()
                .Where(x => x.Value == key)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            PayloadScript = await database.PayloadScripts
                .AsNoTracking()
                .Where(x => x.Id == Attack.PayloadScriptId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            EmailTarget = await database.EmailTargets
                .AsNoTracking()
                .Where(x => x.Id == Attack.EmailTargetId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            PayloadScript.InsertTargetValues(_config, EmailTarget, Attack);

            await LogScriptVisit(HttpContext, Attack);

            return Content(PayloadScript.Script, "application/javascript", Encoding.UTF8);
        }
        catch
        {
            return NotFound();
        }
    }

    public async Task LogScriptVisit(
        HttpContext context, Attack pageKey)
    {
        var visit = new ScriptVisit()
        {
            Url = context.Request.GetDisplayUrl(),
            Method = context.Request.Method,
            IpAddress = context.Connection.RemoteIpAddress?.ToString(),
            UserAgent = context.Request.Headers?.UserAgent,
            PageKeyId = pageKey.Id,
        };

        database.ScriptVisits.Add(visit);

        await database.SaveAsync(UserToken);
    }
}
