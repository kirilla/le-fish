using Lefish.Application.Commands.DataDumps.UploadDataDump;
using Lefish.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;

namespace Lefish.Web.Pages;

[IgnoreAntiforgeryToken]
[AllowAnonymous]
public class UploadPageModel(
    IUserToken userToken,
    IDatabaseService database,
    IUploadDataDumpCommand command) : UserTokenPageModel(userToken)
{
    public async Task<IActionResult> OnPostAsync(int token)
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

                Url = HttpContext.Request.GetDisplayUrl(),
                Method = HttpContext.Request.Method,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserAgent = HttpContext.Request.Headers?.UserAgent,
            };

            await command.Execute(UserToken, commandModel, token);

            return new AcceptedResult();
        }
        catch
        {
            return BadRequest();
        }
    }
}
