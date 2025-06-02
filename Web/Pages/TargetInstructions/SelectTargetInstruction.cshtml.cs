using Lefish.Application.Commands.TargetInstructions.SelectTargetInstruction;

namespace Lefish.Web.Pages.TargetInstructions;

public class SelectTargetInstructionModel(
    IUserToken userToken,
    IDatabaseService database,
    ISelectTargetInstructionCommand command) : UserTokenPageModel(userToken)
{
    public Attack Attack { get; set; }

    public List<InstructionSummary> Instructions { get; set; }

    [BindProperty]
    public SelectTargetInstructionCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Attack = await database.Attacks
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Instructions = await database.Instructions
                .OrderBy(x => x.Name)
                .Select(x => new InstructionSummary()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Created = x.Created,
                })
                .ToListAsync();

            CommandModel = new SelectTargetInstructionCommandModel()
            {
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

            Attack = await database.Attacks
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Instructions = await database.Instructions
                .OrderBy(x => x.Name)
                .Select(x => new InstructionSummary()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Created = x.Created,
                })
                .ToListAsync();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-attack/{id}");
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
}
