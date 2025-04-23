using Lefish.Application.Commands.EmailAttachments.EditEmailAttachment;

namespace Lefish.Web.Pages.EmailAttachments;

public class EditEmailAttachmentModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditEmailAttachmentCommand command) : UserTokenPageModel(userToken)
{
    public EmailAttachment EmailAttachment { get; set; }

    [BindProperty]
    public EditEmailAttachmentCommandModel CommandModel { get; set; }

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

            CommandModel = new EditEmailAttachmentCommandModel()
            {
                EmailAttachmentId = EmailAttachment.Id,
                Name = EmailAttachment.Name,
                ContentType = EmailAttachment.ContentType,
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

            EmailAttachment = await database.EmailAttachments
                .Where(x => x.Id == CommandModel.EmailAttachmentId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-email-attachment/{id}");
        }
        catch (BlockedByNameException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns en annat bilaga med samma namn.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
