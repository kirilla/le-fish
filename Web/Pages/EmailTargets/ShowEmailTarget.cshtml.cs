using Lefish.Application.Commands.DataDumps.UploadDataDump;
using Lefish.Application.Commands.EmailMessages.SendEmailToTarget;
using Lefish.Application.Commands.EmailTargets.EditEmailTarget;
using Lefish.Application.Commands.EmailTargets.RemoveEmailTarget;

namespace Lefish.Web.Pages.EmailTargets;

public class ShowEmailTargetModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditEmailTargetCommand editTargetCommand,
    IRemoveEmailTargetCommand removeTargetCommand,
    ISendEmailToTargetCommand sendEmailToTargetCommand,
    IUploadDataDumpCommand uploadDataDumpCommand) : UserTokenPageModel(userToken)
{
    public EmailTarget EmailTarget { get; set; }

    public List<EmailHeader> EmailHeaders { get; set; }
    public List<PageVisitPlus> PageVisits { get; set; }
    public List<PageToken> PageTokens { get; set; }

    public bool CanEditTarget { get; set; }
        = editTargetCommand.IsPermitted(userToken);

    public bool CanRemoveTarget { get; set; }
        = removeTargetCommand.IsPermitted(userToken);

    public bool CanSendEmailToTargetCommand { get; set; }
        = sendEmailToTargetCommand.IsPermitted(userToken);

    public bool CanUploadDataDump { get; set; }
        = uploadDataDumpCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            EmailTarget = await database.EmailTargets
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            EmailHeaders = await database.EmailMessages
                .Include(x => x.EmailAccount)
                .Include(x => x.EmailTarget)
                .OrderByDescending(x => x.Created)
                .Where(x => x.EmailTargetId == id)
                .Select(x => new EmailHeader()
                {
                    Id = x.Id,
                    ToName = x.EmailTarget.Name,
                    ToAddress = x.EmailTarget.Address,
                    FromName = x.EmailAccount.FromName,
                    FromAddress = x.EmailAccount.FromAddress,
                    ReplyToName = x.EmailAccount.ReplyToName,
                    ReplyToAddress = x.EmailAccount.ReplyToAddress,
                    Subject = x.Subject,
                    EmailStatus = x.EmailStatus,
                    Created = x.Created,
                    Sent = x.Sent,
                })
                .ToListAsync();

            PageVisits = await database.PageVisits
                .Where(x => x.EmailTargetId == id)
                .OrderByDescending(x => x.Created)
                .Select(x => new PageVisitPlus() { 
                    Id = x.Id,
                    Created = x.Created,
                    Url = x.Url,
                    Method = x.Method,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    PageName = x.PayloadPage.Name,
                    PayloadPageId = x.PayloadPageId,
                    TargetName = x.EmailTarget.Name,
                    TargetAddress = x.EmailTarget.Address,
                    EmailTargetId = x.EmailTargetId,
                })
                .ToListAsync();

            PageTokens = await database.PageTokens
                .Where(x => x.EmailMessage.EmailTargetId == id)
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
