using Lefish.Application.Commands.EmailTemplates.CloneEmailTemplate;
using Lefish.Application.Commands.EmailTemplates.EditEmailTemplate;
using Lefish.Application.Commands.EmailTemplates.RemoveEmailTemplate;
using Lefish.Application.Commands.EmailTemplates.UploadEmailAttachment;
using Lefish.Application.Commands.EmailTemplates.UploadEmailImage;

namespace Lefish.Web.Pages.EmailTemplates;

public class ShowEmailTemplateModel(
    IUserToken userToken,
    IDatabaseService database,
    ICloneEmailTemplateCommand cloneTemplateCommand,
    IEditEmailTemplateCommand editTemplateCommand,
    IRemoveEmailTemplateCommand removeTemplateCommand,
    IUploadEmailAttachmentCommand uploadEmailAttachmentCommand,
    IUploadEmailImageCommand uploadEmailImageCommand) : UserTokenPageModel(userToken)
{
    public EmailTemplate EmailTemplate { get; set; }

    public List<EmailAttachment> EmailAttachments { get; set; }
    public List<EmailImage> EmailImages { get; set; }

    public bool CanCloneTemplate { get; set; }
        = cloneTemplateCommand.IsPermitted(userToken);

    public bool CanEditTemplate { get; set; }
        = editTemplateCommand.IsPermitted(userToken);

    public bool CanRemoveTemplate { get; set; }
        = removeTemplateCommand.IsPermitted(userToken);

    public bool CanUploadEmailAttachment { get; set; }
        = uploadEmailAttachmentCommand.IsPermitted(userToken);

    public bool CanUploadEmailImage { get; set; }
        = uploadEmailImageCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            EmailTemplate = await database.EmailTemplates
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            EmailAttachments = await database.EmailAttachments
                .Where(x => x.EmailTemplateId == id)
                .ToListAsync();

            EmailImages = await database.EmailImages
                .Where(x => x.EmailTemplateId == id)
                .ToListAsync();

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
