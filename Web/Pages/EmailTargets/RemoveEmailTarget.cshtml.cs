using Lefish.Application.Commands.EmailTargets.RemoveEmailTarget;

namespace Lefish.Web.Pages.EmailTargets;

public class RemoveEmailTargetModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveEmailTargetCommand command) : UserTokenPageModel(userToken)
{
    public EmailTarget EmailTarget { get; set; }

    [BindProperty]
    public RemoveEmailTargetCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailTarget = await database.Targets
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveEmailTargetCommandModel()
            {
                EmailTargetId = EmailTarget.Id,
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

            EmailTarget = await database.Targets
                .Where(x => x.Id == CommandModel.EmailTargetId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-email-targets");
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
