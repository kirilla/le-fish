using Lefish.Application.Commands.WebPages.AddWebPage;

namespace Lefish.Web.Pages.WebPages;

public class ShowWebPagesModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddWebPageCommand addWebPageCommand) : UserTokenPageModel(userToken)
{
    public List<WebPage> WebPages { get; set; }

    public bool CanAddWebPage { get; set; }
        = addWebPageCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            WebPages = await database.WebPages
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
