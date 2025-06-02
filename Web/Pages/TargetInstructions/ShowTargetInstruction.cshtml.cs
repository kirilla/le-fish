using Lefish.Application.Commands.TargetInstructions.CloneTargetInstruction;
using Lefish.Application.Commands.TargetInstructions.EditTargetInstruction;
using Lefish.Application.Commands.TargetInstructions.RemoveTargetInstruction;

namespace Lefish.Web.Pages.TargetInstructions;

public class ShowTargetInstructionModel(
    IUserToken userToken,
    IDatabaseService database,
    ICloneTargetInstructionCommand cloneTargetInstructionCommand,
    IEditTargetInstructionCommand editTargetInstructionCommand,
    IRemoveTargetInstructionCommand removeTargetInstructionCommand,
    IOptions<TemplateConfiguration> templateConfiguration) : UserTokenPageModel(userToken)
{
    public readonly TemplateConfiguration Config = templateConfiguration.Value;

    public TargetInstruction TargetInstruction { get; set; }

    public bool CanCloneTargetInstruction { get; set; }
        = cloneTargetInstructionCommand.IsPermitted(userToken);

    public bool CanEditTargetInstruction { get; set; }
        = editTargetInstructionCommand.IsPermitted(userToken);

    public bool CanRemoveTargetInstruction { get; set; }
        = removeTargetInstructionCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            TargetInstruction = await database.TargetInstructions
                .Where(x => x.Id == id)
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
