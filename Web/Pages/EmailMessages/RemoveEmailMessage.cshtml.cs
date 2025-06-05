using Lefish.Application.Commands.EmailMessages.RemoveEmailMessage;

namespace Lefish.Web.Pages.EmailMessages;

public class RemoveEmailMessageModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveEmailMessageCommand command) : UserTokenPageModel(userToken)
{
    public EmailMessage EmailMessage { get; set; }

    [BindProperty]
    public RemoveEmailMessageCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailMessage = await database.EmailMessages
                .Include(x => x.EmailAccount)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveEmailMessageCommandModel()
            {
                Id = EmailMessage.Id,
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

            EmailMessage = await database.EmailMessages
                .Include(x => x.Attack)
                .Where(x => x.Id == CommandModel.Id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            var targetId = EmailMessage.Attack.TargetId;

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-email-target/{targetId}");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort meddelandet.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
