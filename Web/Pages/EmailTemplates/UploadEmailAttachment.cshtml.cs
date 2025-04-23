using Lefish.Application.Commands.EmailTemplates.UploadEmailAttachment;

namespace Lefish.Web.Pages.EmailTemplates;

public class UploadEmailAttachmentModel(
    IUserToken userToken,
    IDatabaseService database,
    IUploadEmailAttachmentCommand command) : UserTokenPageModel(userToken)
{
    public EmailTemplate EmailTemplate { get; set; }

    [BindProperty]
    public IFormFile UploadedFile { get; set; }

    public UploadEmailAttachmentCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailTemplate = await database.EmailTemplates
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new UploadEmailAttachmentCommandModel()
            {
                EmailTemplateId = EmailTemplate.Id,
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

            EmailTemplate = await database.EmailTemplates
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            if (UploadedFile == null || UploadedFile.Length == 0)
            {
                ModelState.AddModelError("", "Please upload a valid file.");

                return Page();
            }

            var data = await GetFileBytesAsync(UploadedFile);

            CommandModel = new UploadEmailAttachmentCommandModel()
            {
                EmailTemplateId = id,
                Data = data,
                //ContentLength = data.Length,
                ContentType = UploadedFile.ContentType,
                Name = UploadedFile.FileName,
            };

            CommandModel.Data = await GetFileBytesAsync(UploadedFile);

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-email-template/{id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }

    private async Task<byte[]> GetFileBytesAsync(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        
        await file.CopyToAsync(memoryStream);

        return memoryStream.ToArray();
    }
}
