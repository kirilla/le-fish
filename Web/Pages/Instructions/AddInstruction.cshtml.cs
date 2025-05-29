using Lefish.Application.Commands.Instructions.AddInstruction;

namespace Lefish.Web.Pages.Instructions;

public class AddInstructionModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddInstructionCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public AddInstructionCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddInstructionCommandModel()
            {
                Name = "Instruktion A",
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

            return Redirect($"/show-instruction/{id}");
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
                alert('Instruktion A'); 
            })()
            """;
    }
}
