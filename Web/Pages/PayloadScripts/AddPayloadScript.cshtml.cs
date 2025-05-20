using Lefish.Application.Commands.PayloadScripts.AddPayloadScript;

namespace Lefish.Web.Pages.PayloadScripts;

public class AddPayloadScriptModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddPayloadScriptCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public AddPayloadScriptCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddPayloadScriptCommandModel()
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
            (function(){ alert('sample'); })()
            """;
    }
}
