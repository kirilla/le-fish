using Lefish.Application.Commands.PageScripts.AddPageScript;

namespace Lefish.Web.Pages.PageScripts;

public class AddPageScriptModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddPageScriptCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public AddPageScriptCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddPageScriptCommandModel()
            {
                Name = "Skript X",
                Script = GetDefaultTemplate(),
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

            return Redirect($"/show-payload-script/{id}");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns ett annat skript med samma namn.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }

    private string GetDefaultTemplate()
    {
        return """
            (function() { 
                alert('hej [[attack_token]]'); 
            })()
            """;
    }
}
