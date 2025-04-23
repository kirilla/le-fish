using Lefish.Application.Extensions;
using Lefish.Common.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;

namespace Lefish.Web.Pages;

[AllowAnonymous]
public class PayloadModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public PayloadPage PayloadPage { get; set; }

    public List<PayloadPage> PayloadPages { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            var path = HttpContext.Request.Path;

            if (string.IsNullOrWhiteSpace(path))
                throw new NotFoundException();

            var pages = await database.PayloadPages
                .OrderBy(x => x.Id)
                .Select(x => new
                {
                    x.Id,
                    x.UrlRegex,
                })
                .ToListAsync();

            var matchingPage = pages
                .Where(x => !string.IsNullOrWhiteSpace(x.UrlRegex))
                .Where(x => RegexService.IsMatch(path, x.UrlRegex))
                .FirstOrDefault();

            if (matchingPage == null)
                return Redirect("/help/notfound");

            PayloadPage = await database.PayloadPages
                .AsNoTracking()
                .Where(x => x.Id == matchingPage.Id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            var identifier = RegexService.GetFirstCaptureGroup(path, matchingPage.UrlRegex);

            if (string.IsNullOrWhiteSpace(identifier))
                return Redirect("/help/notfound");

            var target = await database.EmailTargets
                .AsNoTracking()
                .Where(x => x.Identifier == identifier)
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
