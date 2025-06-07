using Lefish.Application.Commands.Instructions.RemoveInstruction;

namespace Lefish.Web.Pages.Instructions;

public class RemoveInstructionModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveInstructionCommand command) : UserTokenPageModel(userToken)
{
    public Instruction Instruction { get; set; }
    public InstructionSet InstructionSet { get; set; }

    [BindProperty]
    public RemoveInstructionCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Instruction = await database.Instructions
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            InstructionSet = await database.InstructionSets
                .Where(x => x.Id == Instruction.InstructionSetId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveInstructionCommandModel()
            {
                InstructionId = Instruction.Id,
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

            Instruction = await database.Instructions
                .Where(x => x.Id == CommandModel.InstructionId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            InstructionSet = await database.InstructionSets
                .Where(x => x.Id == Instruction.InstructionSetId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-instruction-set/{InstructionSet.Id}");
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
