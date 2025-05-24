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
    public PageKey PageKey { get; set; }
    public PayloadScript PayloadScript { get; set; }

    public async Task<IActionResult> OnGetAsync(int key)
    {
        try
        {
            PageKey = await database.PageKeys
                .AsNoTracking()
                .Where(x => x.Token == key)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            PayloadScript = await database.PayloadScripts
                .AsNoTracking()
                .Where(x => x.Id == PageKey.PayloadScriptId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            var emailTargetId = await database.PageKeys
                .AsNoTracking()
                .Where(x => x.Token == key)
                .Select(x => x.EmailMessage.EmailTargetId)
                .Cast<int?>()
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            EmailTarget = await database.EmailTargets
                .AsNoTracking()
                .Where(x => x.Id == emailTargetId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            PayloadScript.InsertTargetValues(_config, EmailTarget, PageKey);

            await LogScriptVisit(HttpContext, PageKey);

            return Content(PayloadScript.Script, "application/javascript", Encoding.UTF8);
        }
        catch
        {
            return NotFound();
        }
    }

    public async Task LogScriptVisit(
        HttpContext context, PageKey pageKey)
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
