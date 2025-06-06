using Lefish.Application.Commands.InstructionSets.RemoveInstructionSet;

namespace Lefish.Web.Pages.InstructionSets;

public class RemoveInstructionSetModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveInstructionSetCommand command) : UserTokenPageModel(userToken)
{
    public InstructionSet InstructionSet { get; set; }

    [BindProperty]
    public RemoveInstructionSetCommandModel CommandModel { get; set; }

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

            CommandModel = new RemoveInstructionSetCommandModel()
            {
                Id = InstructionSet.Id,
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

            InstructionSet = await database.InstructionSets
                .Where(x => x.Id == CommandModel.Id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-instruction-sets");
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
