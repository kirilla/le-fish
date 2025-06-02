using Lefish.Application.Commands.TargetInstructions.RemoveTargetInstruction;

namespace Lefish.Web.Pages.TargetInstructions;

public class RemoveTargetInstructionModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveTargetInstructionCommand command) : UserTokenPageModel(userToken)
{
    public TargetInstruction TargetInstruction { get; set; }

    [BindProperty]
    public RemoveTargetInstructionCommandModel CommandModel { get; set; }

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

            CommandModel = new RemoveTargetInstructionCommandModel()
            {
                TargetInstructionId = TargetInstruction.Id,
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

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            TargetInstruction = await database.TargetInstructions
                .Where(x => x.Id == CommandModel.TargetInstructionId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            var attackId = TargetInstruction.AttackId;

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-attack/{attackId}");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
