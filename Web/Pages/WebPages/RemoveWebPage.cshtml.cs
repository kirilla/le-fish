using Lefish.Application.Commands.WebPages.RemoveWebPage;

namespace Lefish.Web.Pages.WebPages;

public class RemoveWebPageModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveWebPageCommand command) : UserTokenPageModel(userToken)
{
    public WebPage WebPage { get; set; }

    [BindProperty]
    public RemoveWebPageCommandModel CommandModel { get; set; }

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

            CommandModel = new RemoveWebPageCommandModel()
            {
                Id = WebPage.Id,
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

    public async Task<IActionResult> OnPostAsync()
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

            return Redirect($"/show-web-pages");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
