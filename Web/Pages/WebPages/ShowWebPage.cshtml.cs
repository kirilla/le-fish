using Lefish.Application.Commands.WebPages.CloneWebPage;
using Lefish.Application.Commands.WebPages.EditWebPage;
using Lefish.Application.Commands.WebPages.RemoveWebPage;

namespace Lefish.Web.Pages.WebPages;

public class ShowWebPageModel(
    IUserToken userToken,
    IDatabaseService database,
    ICloneWebPageCommand cloneWebPageCommand,
    IEditWebPageCommand editWebPageCommand,
    IRemoveWebPageCommand removeWebPageCommand) : UserTokenPageModel(userToken)
{
    public WebPage WebPage { get; set; }

    public bool CanCloneWebPage { get; set; }
        = cloneWebPageCommand.IsPermitted(userToken);

    public bool CanEditWebPage { get; set; }
        = editWebPageCommand.IsPermitted(userToken);

    public bool CanRemoveWebPage { get; set; }
        = removeWebPageCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            WebPage = await database.WebPages
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

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
}
