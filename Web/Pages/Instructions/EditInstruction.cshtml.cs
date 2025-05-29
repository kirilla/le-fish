using Lefish.Application.Commands.Instructions.EditInstruction;

namespace Lefish.Web.Pages.Instructions;

public class EditInstructionModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditInstructionCommand command) : UserTokenPageModel(userToken)
{
    public Instruction Instruction { get; set; }

    [BindProperty]
    public EditInstructionCommandModel CommandModel { get; set; }

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

            CommandModel = new EditInstructionCommandModel()
            {
                InstructionId = Instruction.Id,
				Name = Instruction.Name,
				Script = Instruction.Script,
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

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-instruction/{id}");
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
