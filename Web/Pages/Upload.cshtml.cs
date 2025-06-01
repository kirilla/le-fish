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
    public async Task<IActionResult> OnPostAsync(int key)
    {
        try
        {
            //if (!command.IsPermitted(UserToken))
            //    throw new NotPermittedException();

            //if (!ModelState.IsValid)
            //    return Page();

            string requestBody;

            using (var reader = new StreamReader(Request.Body))
            {
                requestBody = await reader.ReadToEndAsync();
            }

            var commandModel = new UploadDataDumpCommandModel()
            {
                JsonData = requestBody,
            };

            await command.Execute(UserToken, commandModel, key);

            return new AcceptedResult();
        }
        catch
        {
            return BadRequest();
        }
    }
}
