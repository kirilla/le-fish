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

    public List<EmailHeader> EmailMessages { get; set; }
    public List<PageKeyPlus> PageTokens { get; set; }
    public List<Visit> Visits { get; set; }

    public List<string> IpAddresses { get; set; }
    public List<string> UserAgents { get; set; }
    
    public bool CanEditTarget { get; set; }
        = editTargetCommand.IsPermitted(userToken);

    public bool CanRemoveTarget { get; set; }
        = removeTargetCommand.IsPermitted(userToken);

    public bool CanSendEmailToTarget { get; set; }
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

            EmailMessages = await database.EmailMessages
                .Include(x => x.EmailAccount)
                .Include(x => x.EmailTarget)
                .Where(x => x.EmailTargetId == id)
                .OrderByDescending(x => x.Created)
                .Select(x => new EmailHeader()
                {
                    Id = x.Id,
                    EmailTargetId = x.EmailTargetId,
                    //ToName = x.EmailTarget.Name,
                    //ToAddress = x.EmailTarget.Address,
                    //FromName = x.EmailAccount.FromName,
                    //FromAddress = x.EmailAccount.FromAddress,
                    //ReplyToName = x.EmailAccount.ReplyToName,
                    //ReplyToAddress = x.EmailAccount.ReplyToAddress,
                    Subject = x.Subject,
                    EmailStatus = x.EmailStatus,
                    //Created = x.Created,
                    Sent = x.Sent,
                })
                .ToListAsync();

            var pageVisits = await database.PageVisits
                .OrderBy(x => x.Created)
                .Where(x => x.PageKey.EmailMessage.EmailTargetId == id)
                .Select(x => new Visit()
                {
                    VisitKind = VisitKind.Page,
                    Id = x.Id,
                    PageTokenId = x.PageKeyId,
                    Created = x.Created,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    PageName = x.PageKey.PayloadPage.Name,
                    PayloadPageId = x.PageKey.PayloadPageId,
                })
                .ToListAsync();

            var scriptVisits = await database.ScriptVisits
                .OrderBy(x => x.Created)
                .Where(x => x.PageKey.EmailMessage.EmailTargetId == id)
                .Select(x => new Visit()
                {
                    VisitKind = VisitKind.Script,
                    Id = x.Id,
                    PageTokenId = x.PageKeyId,
                    Created = x.Created,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    ScriptName = x.PageKey.PayloadScript.Name,
                    PayloadScriptId = x.PageKey.PayloadScriptId,
                })
                .ToListAsync();

            Visits = pageVisits
                .Union(scriptVisits)
                .OrderBy(x => x.Created)
                .ToList();

            PageTokens = await database.PageKeys
                .Where(x => x.EmailMessage.EmailTargetId == id)
                .OrderBy(x => x.Created)
                .Select(x => new PageKeyPlus()
                {
                    Id = x.Id,
                    Token = x.Token,
                    //Created = x.Created,
                    //PageName = x.PayloadPage.Name,
                    PayloadPageId = x.PayloadPageId,
                    TargetName = x.EmailMessage.EmailTarget.Name,
                    TargetAddress = x.EmailMessage.EmailTarget.Address,
                    EmailMessageId = x.EmailMessageId,
                    EmailMessageSubject = x.EmailMessage.Subject,
                    EmailTargetId = x.EmailMessage.EmailTargetId,
                    //PageVisitCount = x.PageVisits.Count(),
                })
                .ToListAsync();

            IpAddresses = Visits
                .Select(x => x.IpAddress)
                .Where(x => x != null)
                .Cast<string>()
                .Distinct()
                .ToList();

            UserAgents = Visits
                .Select(x => x.UserAgent)
                .Where(x => x != null)
                .Cast<string>()
                .Distinct()
                .ToList();

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
