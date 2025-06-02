using Lefish.Application.Commands.TargetInstructions.AddTargetInstruction;

namespace Lefish.Web.Pages.TargetInstructions;

public class AddTargetInstructionModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddTargetInstructionCommand command) : UserTokenPageModel(userToken)
{
    public PageKey PageKey { get; set; }

    [BindProperty]
    public AddTargetInstructionCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            PageKey = await database.Attacks
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new AddTargetInstructionCommandModel()
            {
                Name = "Instruktion A",
                Script = GetDefaultTemplate(),
                PageKeyId = id,
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

            PageKey = await database.Attacks
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-page-key/{id}");
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

    private string GetDefaultTemplate()
    {
        return """
            (function() { 
                alert('Instruktion A'); 
            })();
            """;
    }
}
