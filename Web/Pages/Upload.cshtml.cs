using Lefish.Application.Commands.DataDumps.UploadDataDump;
using Microsoft.AspNetCore.Authorization;

namespace Lefish.Web.Pages;

[AllowAnonymous]
public class UploadPageModel(
    IUserToken userToken,
    IDatabaseService database,
    IUploadDataDumpCommand command) : UserTokenPageModel(userToken)
{
    public EmailTarget EmailTarget { get; set; }

    [BindProperty]
    public IFormFile UploadedFile { get; set; }

    public UploadDataDumpCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            //if (!command.IsPermitted(UserToken))
            //    throw new NotPermittedException();

            EmailTarget = await database.EmailTargets
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new UploadDataDumpCommandModel()
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

    public async Task<IActionResult> OnPostAsync(int id)
    {
        try
        {
            //if (!command.IsPermitted(UserToken))
            //    throw new NotPermittedException();

            EmailTarget = await database.EmailTargets
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

            CommandModel = new UploadDataDumpCommandModel()
            {
                EmailTargetId = id,
                Data = data,
                //ContentLength = data.Length,
                ContentType = UploadedFile.ContentType,
                Name = UploadedFile.FileName,
            };

            CommandModel.Data = await GetFileBytesAsync(UploadedFile);

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-data-dump/{id}");
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
