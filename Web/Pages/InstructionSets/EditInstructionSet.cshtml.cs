using Lefish.Application.Commands.InstructionSets.EditInstructionSet;

namespace Lefish.Web.Pages.InstructionSets;

public class EditInstructionSetModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditInstructionSetCommand command) : UserTokenPageModel(userToken)
{
    public InstructionSet InstructionSet { get; set; }

    [BindProperty]
    public EditInstructionSetCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            InstructionSet = await database.InstructionSets
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditInstructionSetCommandModel()
            {
                Id = InstructionSet.Id,
                Name = InstructionSet.Name,
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

            InstructionSet = await database.InstructionSets
                .Where(x => x.Id == CommandModel.Id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-instruction-set/{id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
