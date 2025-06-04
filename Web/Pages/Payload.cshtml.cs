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

            var payloadPage = await database.PayloadPages
                .AsNoTracking()
                .Where(x => x.Id == attack.PayloadPageId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            var emailTarget = await database.EmailTargets
                .AsNoTracking()
                .Where(x => x.Id == attack.EmailTargetId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            HtmlPage = payloadPage.ReplaceVariables(_config, emailTarget, attack);

            await LogPageVisit(HttpContext, attack);

            return Page();
        }
        catch
        {
            return NotFound();
        }
    }

    public async Task LogPageVisit(
        HttpContext context, Attack attack)
    {
        var visit = new PageVisit()
        {
            Url = context.Request.GetDisplayUrl(),
            Method = context.Request.Method,
            IpAddress = context.Connection.RemoteIpAddress?.ToString(),
            UserAgent = context.Request.Headers?.UserAgent,
            AttackId = attack.Id,
        };

        database.Visits.Add(visit);

        await database.SaveAsync(UserToken);
    }
}
