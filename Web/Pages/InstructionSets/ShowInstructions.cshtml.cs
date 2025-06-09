using Lefish.Application.Commands.InstructionSets.AddInstructionSet;
using Lefish.Application.Commands.InstructionSets.CloneInstructionSet;

namespace Lefish.Web.Pages.InstructionSets;

public class ShowInstructionsModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddInstructionSetCommand addInstructionSetCommand,
    ICloneInstructionSetCommand cloneInstructionSetCommand) : UserTokenPageModel(userToken)
{
    public List<InstructionSet> InstructionSets { get; set; }
    public List<InstructionSummary> Instructions { get; set; }

    public bool CanAddInstructionSet { get; set; }
        = addInstructionSetCommand.IsPermitted(userToken);

    public bool CanCloneInstructionSet { get; set; }
        = cloneInstructionSetCommand.IsPermitted(userToken);

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

            Instructions = await database.Instructions
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .Select(x => new InstructionSummary
                {
                    Id = x.Id,
                    InstructionSetId = x.InstructionSetId,
                    Name = x.Name,
                    Created = x.Created,
                })
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
