using Lefish.Application.Commands.EmailMessages.SendEmailToTarget;
using Lefish.Application.Commands.EmailTargets.EditEmailTarget;
using Lefish.Application.Commands.EmailTargets.RemoveEmailTarget;

namespace Lefish.Web.Pages.EmailTargets;

public class ShowEmailTargetModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditEmailTargetCommand editTargetCommand,
    IRemoveEmailTargetCommand removeTargetCommand,
    ISendEmailToTargetCommand sendEmailToTargetCommand) : UserTokenPageModel(userToken)
{
    public EmailTarget EmailTarget { get; set; }

    public List<EmailHeader> EmailMessages { get; set; }
    public List<PageKeyPlus> PageKeys { get; set; }

    public bool CanEditTarget { get; set; }
        = editTargetCommand.IsPermitted(userToken);

    public bool CanRemoveTarget { get; set; }
        = removeTargetCommand.IsPermitted(userToken);

    public bool CanSendEmailToTarget { get; set; }
        = sendEmailToTargetCommand.IsPermitted(userToken);

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

            PageKeys = await database.PageKeys
                .Where(x => x.EmailMessage.EmailTargetId == id)
                .OrderBy(x => x.Created)
                .Select(x => new PageKeyPlus()
                {
                    Id = x.Id,
                    Value = x.Value,
                    //Created = x.Created,
                    PageName = x.PayloadPage.Name,
                    PayloadPageId = x.PayloadPageId,
                    TargetName = x.EmailMessage.EmailTarget.Name,
                    TargetAddress = x.EmailMessage.EmailTarget.Address,
                    EmailMessageId = x.EmailMessageId,
                    EmailMessageSubject = x.EmailMessage.Subject,
                    EmailTargetId = x.EmailMessage.EmailTargetId,
                    //PageVisitCount = x.PageVisits.Count(),
                    ScriptName = x.PayloadScript.Name,
                    PayloadScriptId = x.PayloadScriptId,
                })
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
