using Lefish.Application.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;

namespace Lefish.Web.Pages;

[IgnoreAntiforgeryToken]
[AllowAnonymous]
public class PayloadPageModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public EmailTarget EmailTarget { get; set; }
    public PayloadPage PayloadPage { get; set; }

    public List<PayloadPage> PayloadPages { get; set; }

    public async Task<IActionResult> OnGetAsync(int token)
    {
        try
        {
            var phishingToken = await database.PhishingTokens
                .AsNoTracking()
                .Include(x => x.EmailTarget)
                .Include(x => x.PayloadPage)
                .Where(x => x.Token == token)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            EmailTarget = phishingToken.EmailTarget;
            PayloadPage = phishingToken.PayloadPage;

            PayloadPage.InsertTargetValues(EmailTarget, phishingToken);

            await LogPageVisit(HttpContext, PayloadPage, EmailTarget);

            return Page();
        }
        catch
        {
            return NotFound();
        }
    }

    public async Task LogPageVisit(
        HttpContext context, PayloadPage page, EmailTarget target)
    {
        var visit = new PageVisit()
        {
            Url = context.Request.GetDisplayUrl(),
            Method = context.Request.Method,
            IpAddress = context.Connection.RemoteIpAddress?.ToString(),
            UserAgent = context.Request.Headers?.UserAgent,
            PayloadPageId = page.Id,
            EmailTargetId = target.Id,
        };

        database.PageVisits.Add(visit);

        await database.SaveAsync(UserToken);
    }
}
