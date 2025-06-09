using Lefish.Application.Commands.InstructionSets.CloneInstructionSet;

namespace Lefish.Web.Pages.InstructionSets;

public class CloneInstructionSetModel(
    IUserToken userToken,
    IDatabaseService database,
    ICloneInstructionSetCommand command) : UserTokenPageModel(userToken)
{
    public List<InstructionSet> InstructionSets { get; set; }

    [BindProperty]
    public CloneInstructionSetCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            InstructionSets = await database.InstructionSets
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();

            CommandModel = new CloneInstructionSetCommandModel()
            {
                Name = "Ny samling",
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

            InstructionSets = await database.InstructionSets
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-instructions");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Ge samlingen ett unikt namn.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
