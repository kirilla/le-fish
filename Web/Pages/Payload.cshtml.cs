using Lefish.Application.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;

namespace Lefish.Web.Pages;

[IgnoreAntiforgeryToken]
[AllowAnonymous]
public class PayloadPageModel(
    IUserToken userToken,
    IDatabaseService database,
    IOptions<TemplateConfiguration> templateConfiguration) : UserTokenPageModel(userToken)
{
    private readonly TemplateConfiguration _config = templateConfiguration.Value;

    public string HtmlPage {  get; set; }

    public async Task<IActionResult> OnGetAsync(int token)
    {
        try
        {
            var attack = await database.Attacks
                .AsNoTracking()
                .Where(x => x.Value == token)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            var payloadPage = await database.WebPages
                .AsNoTracking()
                .Where(x => x.Id == attack.PayloadPageId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            var target = await database.Targets
                .AsNoTracking()
                .Where(x => x.Id == attack.TargetId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            HtmlPage = payloadPage.ReplaceVariables(_config, target, attack);

            await LogEvent(HttpContext, attack);

            return Page();
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
            AttackEventKind = AttackEventKind.PageVisit,
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
