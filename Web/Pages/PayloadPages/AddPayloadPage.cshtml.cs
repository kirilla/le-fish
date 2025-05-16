using Lefish.Application.Commands.PayloadPages.AddPayloadPage;

namespace Lefish.Web.Pages.PayloadPages;

public class AddPayloadPageModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddPayloadPageCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public AddPayloadPageCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddPayloadPageCommandModel()
            {
                Name = Random.Shared.Next().ToString(),
            };

            return Page();
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

            if (!ModelState.IsValid)
                return Page();

            var id = await command.Execute(UserToken, CommandModel);

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
