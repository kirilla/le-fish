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
    [BindProperty]
    public IFormFile File { get; set; }

    public UploadDataDumpCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int key)
    {
        try
        {
            //if (!command.IsPermitted(UserToken))
            //    throw new NotPermittedException();

            CommandModel = new UploadDataDumpCommandModel();

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

    public async Task<IActionResult> OnPostAsync(int key)
    {
        try
        {
            //if (!command.IsPermitted(UserToken))
            //    throw new NotPermittedException();

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
                Data = data,
                //ContentLength = data.Length,
                ContentType = File.ContentType,
                Name = File.FileName,
            };

            CommandModel.Data = await GetFileBytesAsync(File);

            await command.Execute(UserToken, CommandModel, key);

            return Redirect($"/upload/{key}");
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
