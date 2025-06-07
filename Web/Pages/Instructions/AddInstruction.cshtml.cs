using Lefish.Application.Commands.Instructions.AddInstruction;

namespace Lefish.Web.Pages.Instructions;

public class AddInstructionModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddInstructionCommand command) : UserTokenPageModel(userToken)
{
    public InstructionSet InstructionSet { get; set; }

    [BindProperty]
    public AddInstructionCommandModel CommandModel { get; set; }

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

            CommandModel = new AddInstructionCommandModel()
            {
                Name = "Instruktion A",
                Script = GetDefaultTemplate(),
                InstructionSetId = id,
            };

            return Page();
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
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-instruction-set/{InstructionSet.Id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }

    private string GetDefaultTemplate()
    {
        return """
            (function() { 
                alert('Instruktion A'); 
            })();
            """;
    }
}
