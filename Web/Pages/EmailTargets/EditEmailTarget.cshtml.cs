using Lefish.Application.Commands.EmailTargets.EditEmailTarget;

namespace Lefish.Web.Pages.EmailTargets;

public class EditEmailTargetModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditEmailTargetCommand command) : UserTokenPageModel(userToken)
{
    public EmailTarget EmailTarget { get; set; }

    [BindProperty]
    public EditEmailTargetCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailTarget = await database.EmailTargets
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditEmailTargetCommandModel()
            {
                EmailTargetId = EmailTarget.Id,
                Name = EmailTarget.Name,
                Address = EmailTarget.Address,
                Identifier = EmailTarget.Identifier,
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

            EmailTarget = await database.EmailTargets
                .Where(x => x.Id == CommandModel.EmailTargetId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-email-target/{id}");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Address),
                "Det finns ett annat målkonto med samma adress.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
