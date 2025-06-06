using Lefish.Application.Commands.InstructionSets.AddInstructionSet;

namespace Lefish.Web.Pages.InstructionSets;

public class AddInstructionSetModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddInstructionSetCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public AddInstructionSetCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddInstructionSetCommandModel();

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

            return Redirect($"/show-instruction-set/{id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
