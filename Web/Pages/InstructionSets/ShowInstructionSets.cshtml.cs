using Lefish.Application.Commands.InstructionSets.AddInstructionSet;

namespace Lefish.Web.Pages.InstructionSets;

public class ShowInstructionSetsModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddInstructionSetCommand addInstructionSetCommand) : UserTokenPageModel(userToken)
{
    public List<InstructionSet> InstructionSets { get; set; }

    public bool CanAddInstructionSet { get; set; }
        = addInstructionSetCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            InstructionSets = await database.InstructionSets
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
