using Lefish.Application.Commands.Instructions.CloneInstruction;

namespace Lefish.Web.Pages.Instructions;

public class CloneInstructionModel(
    IUserToken userToken,
    IDatabaseService database,
    ICloneInstructionCommand command) : UserTokenPageModel(userToken)
{
    public Instruction Instruction { get; set; }

    [BindProperty]
    public CloneInstructionCommandModel CommandModel { get; set; }

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

            CommandModel = new CloneInstructionCommandModel()
            {
                InstructionId = Instruction.Id,
                Name = Instruction.Name,
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

            Instruction = await database.Instructions
                .Where(x => x.Id == CommandModel.InstructionId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            var cloneId = await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-instruction/{cloneId}");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns ett skript med samma namn.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
