using Lefish.Application.Commands.EmailAttachments.EditEmailAttachment;
using Lefish.Application.Commands.EmailAttachments.RemoveEmailAttachment;

namespace Lefish.Web.Pages.EmailAttachments;

public class ShowEmailAttachmentModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditEmailAttachmentCommand editEmailAttachmentCommand,
    IRemoveEmailAttachmentCommand removeEmailAttachmentCommand) : UserTokenPageModel(userToken)
{
    public EmailAttachment EmailAttachment { get; set; }

    public bool CanEditEmailAttachment { get; set; }
        = editEmailAttachmentCommand.IsPermitted(userToken);

    public bool CanRemoveEmailAttachment { get; set; }
        = removeEmailAttachmentCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            EmailAttachment = await database.EmailAttachments
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

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
}
