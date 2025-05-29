using Lefish.Application.Commands.Instructions.AddInstruction;

namespace Lefish.Web.Pages.Instructions;

public class ShowInstructionsModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddInstructionCommand addInstructionCommand) : UserTokenPageModel(userToken)
{
    public List<Instruction> Instructions { get; set; }

    public bool CanAddInstruction { get; set; }
        = addInstructionCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            Instructions = await database.Instructions
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
