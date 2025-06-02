using Lefish.Application.Commands.TargetInstructions.CloneTargetInstruction;

namespace Lefish.Web.Pages.TargetInstructions;

public class CloneTargetInstructionModel(
    IUserToken userToken,
    IDatabaseService database,
    ICloneTargetInstructionCommand command) : UserTokenPageModel(userToken)
{
    public TargetInstruction TargetInstruction { get; set; }

    [BindProperty]
    public CloneTargetInstructionCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            TargetInstruction = await database.TargetInstructions
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new CloneTargetInstructionCommandModel()
            {
                Id = TargetInstruction.Id,
                Name = TargetInstruction.Name,
            };

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

    public async Task<IActionResult> OnPostAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            TargetInstruction = await database.TargetInstructions
                .Where(x => x.Id == CommandModel.Id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            var cloneId = await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-target-instruction/{cloneId}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
