using Lefish.Application.Commands.Sessions.EndUserSessions;

namespace Lefish.Web.Pages.Sessions;

public class EndAllSessionsModel(
    IUserToken userToken,
    IDatabaseService database,
    IEndUserSessionsCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public EndUserSessionsCommandModel CommandModel { get; set; }

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new EndUserSessionsCommandModel();

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

            return Redirect("/desktop");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
