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

    public EmailTarget EmailTarget { get; set; }
    public PageKey PageKey { get; set; }
    public PayloadPage PayloadPage { get; set; }

    public async Task<IActionResult> OnGetAsync(int key)
    {
        try
        {
            PageKey = await database.PageKeys
                .AsNoTracking()
                .Where(x => x.Token == key)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            PayloadPage = await database.PayloadPages
                .AsNoTracking()
                .Where(x => x.Id == PageKey.PayloadPageId)
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

            PayloadPage.InsertTargetValues(_config, EmailTarget, PageKey);

            await LogPageVisit(HttpContext, PageKey);

            return Page();
        }
        catch
        {
            return NotFound();
        }
    }

    public async Task LogPageVisit(
        HttpContext context, PageKey pageKey)
    {
        var visit = new PageVisit()
        {
            Url = context.Request.GetDisplayUrl(),
            Method = context.Request.Method,
            IpAddress = context.Connection.RemoteIpAddress?.ToString(),
            UserAgent = context.Request.Headers?.UserAgent,
            PageKeyId = pageKey.Id,
        };

        database.PageVisits.Add(visit);

        await database.SaveAsync(UserToken);
    }
}
