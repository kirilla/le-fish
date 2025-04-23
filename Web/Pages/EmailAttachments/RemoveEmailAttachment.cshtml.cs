using Lefish.Application.Commands.EmailAttachments.RemoveEmailAttachment;

namespace Lefish.Web.Pages.EmailAttachments;

public class RemoveEmailAttachmentModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveEmailAttachmentCommand command) : UserTokenPageModel(userToken)
{
    public EmailAttachment EmailAttachment { get; set; }

    [BindProperty]
    public RemoveEmailAttachmentCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailAttachment = await database.EmailAttachments
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveEmailAttachmentCommandModel()
            {
                EmailAttachmentId = EmailAttachment.Id,
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

            EmailAttachment = await database.EmailAttachments
                .Where(x => x.Id == CommandModel.EmailAttachmentId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-email-template/{EmailAttachment.EmailTemplateId}");
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
