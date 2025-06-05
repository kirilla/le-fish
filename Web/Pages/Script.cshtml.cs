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

    public Target Target { get; set; }
    public Attack Attack { get; set; }
    public PayloadScript PayloadScript { get; set; }

    public async Task<IActionResult> OnGetAsync(int token)
    {
        try
        {
            Attack = await database.Attacks
                .AsNoTracking()
                .Where(x => x.Value == token)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            PayloadScript = await database.PayloadScripts
                .AsNoTracking()
                .Where(x => x.Id == Attack.PayloadScriptId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Target = await database.Targets
                .AsNoTracking()
                .Where(x => x.Id == Attack.TargetId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            var script = PayloadScript.ReplaceVariables(_config, Target, Attack);

            await LogEvent(HttpContext, Attack);

            return Content(script, "application/javascript", Encoding.UTF8);
        }
        catch
        {
            return NotFound();
        }
    }

    public async Task LogEvent(
        HttpContext context, Attack attack)
    {
        var evt = new AttackEvent()
        {
            AttackEventKind = AttackEventKind.ScriptDownload,
            Url = context.Request.GetDisplayUrl(),
            Method = context.Request.Method,
            IpAddress = context.Connection.RemoteIpAddress?.ToString(),
            UserAgent = context.Request.Headers?.UserAgent,
            AttackId = attack.Id,
        };

        database.AttackEvents.Add(evt);

        await database.SaveAsync(UserToken);
    }
}
