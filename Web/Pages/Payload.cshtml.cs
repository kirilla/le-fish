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
    public PayloadPage PayloadPage { get; set; }

    public List<PayloadPage> PayloadPages { get; set; }

    public async Task<IActionResult> OnGetAsync(string pageKey, string personKey)
    {
        try
        {
            PayloadPage = await database.PayloadPages
                .AsNoTracking()
                .Where(x => x.PageKey == pageKey)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            var target = await database.EmailTargets
                .AsNoTracking()
                .Where(x => x.PersonKey == personKey)
                .FirstOrDefaultAsync();

            if (target == null)
                return Redirect("/help/notfound");

            PayloadPage.InsertTargetValues(target);

            await LogPageVisit(HttpContext, PayloadPage, target);

            return Page();
        }
        catch (NotFoundException)
        {
            return Redirect("/help/notfound");
        }
        catch
        {
            return Redirect("/help/notpermitted");
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
