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
    public Attack Attack { get; set; }
    public PayloadPage PayloadPage { get; set; }

    public async Task<IActionResult> OnGetAsync(int key)
    {
        try
        {
            Attack = await database.Attacks
                .AsNoTracking()
                .Where(x => x.Value == key)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            PayloadPage = await database.PayloadPages
                .AsNoTracking()
                .Where(x => x.Id == Attack.PayloadPageId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            EmailTarget = await database.EmailTargets
                .AsNoTracking()
                .Where(x => x.Id == Attack.EmailTargetId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            PayloadPage.InsertTargetValues(_config, EmailTarget, Attack);

            await LogPageVisit(HttpContext, Attack);

            return Page();
        }
        catch
        {
            return NotFound();
        }
    }

    public async Task LogPageVisit(
        HttpContext context, Attack pageKey)
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
