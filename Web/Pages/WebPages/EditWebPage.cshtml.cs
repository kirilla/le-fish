using Lefish.Application.Commands.WebPages.EditWebPage;

namespace Lefish.Web.Pages.WebPages;

public class EditWebPageModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditWebPageCommand command) : UserTokenPageModel(userToken)
{
    public WebPage WebPage { get; set; }

    [BindProperty]
    public EditWebPageCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            WebPage = await database.WebPages
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditWebPageCommandModel()
            {
                Id = WebPage.Id,
				Name = WebPage.Name,
				Html = WebPage.Html,
            };

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

    public async Task<IActionResult> OnPostAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            WebPage = await database.WebPages
                .Where(x => x.Id == CommandModel.Id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-web-page/{id}");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns en annan sida med samma namn.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
