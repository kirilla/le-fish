using Lefish.Application.Commands.DataDumps.UploadDataDump;
using Microsoft.AspNetCore.Authorization;

namespace Lefish.Web.Pages;

[IgnoreAntiforgeryToken]
[AllowAnonymous]
public class UploadPageModel(
    IUserToken userToken,
    IDatabaseService database,
    IUploadDataDumpCommand command) : UserTokenPageModel(userToken)
{
    public EmailTarget EmailTarget { get; set; }

    [BindProperty]
    public IFormFile File { get; set; }

    public UploadDataDumpCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(string personKey)
    {
        try
        {
            //if (!command.IsPermitted(UserToken))
            //    throw new NotPermittedException();

            EmailTarget = await database.EmailTargets
                .Where(x => x.PersonKey == personKey)
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

    public async Task<IActionResult> OnPostAsync(string personKey)
    {
        try
        {
            //if (!command.IsPermitted(UserToken))
            //    throw new NotPermittedException();

            EmailTarget = await database.EmailTargets
                .Where(x => x.PersonKey == personKey)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            if (File == null || File.Length == 0)
            {
                ModelState.AddModelError("", "Please upload a valid file.");

                return Page();
            }

            var data = await GetFileBytesAsync(File);

            CommandModel = new UploadDataDumpCommandModel()
            {
                EmailTargetId = EmailTarget.Id,
                Data = data,
                //ContentLength = data.Length,
                ContentType = File.ContentType,
                Name = File.FileName,
            };

            CommandModel.Data = await GetFileBytesAsync(File);

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/upload/{personKey}");
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
