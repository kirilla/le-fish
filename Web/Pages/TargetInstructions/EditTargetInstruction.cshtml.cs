using Lefish.Application.Commands.TargetInstructions.EditTargetInstruction;

namespace Lefish.Web.Pages.TargetInstructions;

public class EditTargetInstructionModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditTargetInstructionCommand command) : UserTokenPageModel(userToken)
{
    public TargetInstruction TargetInstruction { get; set; }

    [BindProperty]
    public EditTargetInstructionCommandModel CommandModel { get; set; }

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

            CommandModel = new EditTargetInstructionCommandModel()
            {
                TargetInstructionId = TargetInstruction.Id,
				Name = TargetInstruction.Name,
				Script = TargetInstruction.Script,
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
                .Where(x => x.Id == CommandModel.TargetInstructionId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-target-instruction/{id}");
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
}
