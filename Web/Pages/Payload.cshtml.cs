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
    public PageToken PageToken { get; set; }
    public PayloadPage PayloadPage { get; set; }

    public List<PayloadPage> PayloadPages { get; set; }

    public async Task<IActionResult> OnGetAsync(int token)
    {
        try
        {
            PageToken = await database.PageTokens
                .AsNoTracking()
                .Where(x => x.Token == token)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            PayloadPage = await database.PayloadPages
                .AsNoTracking()
                .Where(x => x.Id == PageToken.PayloadPageId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            var emailTargetId = await database.PageTokens
                .AsNoTracking()
                .Where(x => x.Token == token)
                .Select(x => x.EmailMessage.EmailTargetId)
                .Cast<int?>()
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            EmailTarget = await database.EmailTargets
                .AsNoTracking()
                .Where(x => x.Id == emailTargetId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            PayloadPage.InsertTargetValues(EmailTarget, PageToken);

            await LogPageVisit(HttpContext, PageToken);

            return Page();
        }
        catch
        {
            return NotFound();
        }
    }

    public async Task LogPageVisit(
        HttpContext context, PageToken pageToken)
    {
        var visit = new PageVisit()
        {
            Url = context.Request.GetDisplayUrl(),
            Method = context.Request.Method,
            IpAddress = context.Connection.RemoteIpAddress?.ToString(),
            UserAgent = context.Request.Headers?.UserAgent,
            PageTokenId = pageToken.Id,
        };

        database.PageVisits.Add(visit);

        await database.SaveAsync(UserToken);
    }
}
