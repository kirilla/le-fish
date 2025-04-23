using Lefish.Application.Commands.PayloadPages.EditPayloadPage;

namespace Lefish.Web.Pages.PayloadPages;

public class EditPayloadPageModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditPayloadPageCommand command) : UserTokenPageModel(userToken)
{
    public PayloadPage PayloadPage { get; set; }

    [BindProperty]
    public EditPayloadPageCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            PayloadPage = await database.PayloadPages
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditPayloadPageCommandModel()
            {
                PayloadPageId = PayloadPage.Id,
                Name = PayloadPage.Name,
				UrlRegex = PayloadPage.UrlRegex,
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

            PayloadPage = await database.PayloadPages
                .Where(x => x.Id == CommandModel.PayloadPageId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-payload-page/{id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
