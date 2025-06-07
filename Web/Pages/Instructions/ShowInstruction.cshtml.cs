using Lefish.Application.Commands.Instructions.EditInstruction;
using Lefish.Application.Commands.Instructions.RemoveInstruction;

namespace Lefish.Web.Pages.Instructions;

public class ShowInstructionModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditInstructionCommand editInstructionCommand,
    IRemoveInstructionCommand removeInstructionCommand,
    IOptions<TemplateConfiguration> templateConfiguration) : UserTokenPageModel(userToken)
{
    public readonly TemplateConfiguration Config = templateConfiguration.Value;

    public Instruction Instruction { get; set; }
    public InstructionSet InstructionSet { get; set; }

    public bool CanEditInstruction { get; set; }
        = editInstructionCommand.IsPermitted(userToken);

    public bool CanRemoveInstruction { get; set; }
        = removeInstructionCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            Instruction = await database.Instructions
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            InstructionSet = await database.InstructionSets
                .Where(x => x.Id == Instruction.InstructionSetId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

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
