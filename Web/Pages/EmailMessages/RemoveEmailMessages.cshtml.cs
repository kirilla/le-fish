using Lefish.Application.Commands.EmailMessages.RemoveEmailMessages;

namespace Lefish.Web.Pages.EmailMessages;

public class RemoveEmailMessagesModel(
    IUserToken userToken,
    IRemoveEmailMessagesCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public RemoveEmailMessagesCommandModel CommandModel { get; set; }

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new RemoveEmailMessagesCommandModel();

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

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/show-email-queue");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
