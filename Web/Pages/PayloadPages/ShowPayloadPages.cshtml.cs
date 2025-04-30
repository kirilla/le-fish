using Lefish.Application.Commands.PayloadPages.AddPayloadPage;

namespace Lefish.Web.Pages.PayloadPages;

public class ShowPayloadPagesModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddPayloadPageCommand addPayloadPageCommand) : UserTokenPageModel(userToken)
{
    public List<PayloadPage> PayloadPages { get; set; }

    public bool CanAddPayloadPage { get; set; }
        = addPayloadPageCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            PayloadPages = await database.PayloadPages
                .AsNoTracking()
                .OrderBy(x => x.PageKey)
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
