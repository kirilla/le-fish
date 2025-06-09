using Lefish.Application.Commands.Instructions.AddInstruction;
using Lefish.Application.Commands.InstructionSets.EditInstructionSet;
using Lefish.Application.Commands.InstructionSets.RemoveInstructionSet;

namespace Lefish.Web.Pages.InstructionSets;

public class ShowInstructionSetModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddInstructionCommand addInstructionCommand,
    IEditInstructionSetCommand editInstructionSetCommand,
    IRemoveInstructionSetCommand removeInstructionSetCommand) : UserTokenPageModel(userToken)
{
    public InstructionSet InstructionSet { get; set; }

    public List<Instruction> Instructions { get; set; } = new List<Instruction>();

    public bool CanAddInstruction { get; set; }
        = addInstructionCommand.IsPermitted(userToken);

    public bool CanEditInstructionSet { get; set; }
        = editInstructionSetCommand.IsPermitted(userToken);

    public bool CanRemoveInstructionSet { get; set; }
        = removeInstructionSetCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            InstructionSet = await database.InstructionSets
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Instructions = await database.Instructions
                .Where(x => x.InstructionSetId == id)
                .ToListAsync();

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
}
