using Lefish.Application.Commands.PayloadPages.EditWebPage;

namespace Lefish.Web.Pages.PayloadPages;

public class EditPayloadPageModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditWebPageCommand command) : UserTokenPageModel(userToken)
{
    public WebPage PayloadPage { get; set; }

    [BindProperty]
    public EditWebPageCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            PayloadPage = await database.WebPages
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditWebPageCommandModel()
            {
                PayloadPageId = PayloadPage.Id,
				Name = PayloadPage.Name,
				Html = PayloadPage.Html,
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

            PayloadPage = await database.WebPages
                .Where(x => x.Id == CommandModel.PayloadPageId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-payload-page/{id}");
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
