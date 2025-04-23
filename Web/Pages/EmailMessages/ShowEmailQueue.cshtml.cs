using static Lefish.Common.Validation.Pattern.Common;

namespace Lefish.Web.Pages.EmailMessages;

public class ShowEmailQueueModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public List<EmailHeader> NotSentEmails { get; set; }
    public List<EmailHeader> FailedEmails { get; set; }
    public List<EmailHeader> SentEmails { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            var emails = await database.EmailMessages
                .Include(x => x.EmailAccount)
                .Include(x => x.EmailTarget)
                .OrderBy(x => x.Created)
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

            NotSentEmails = emails
                .Where(x => x.EmailStatus == EmailStatus.NotSent)
                .ToList();

            FailedEmails = emails
                .Where(x => x.EmailStatus == EmailStatus.SendFailed)
                .ToList();

            SentEmails = emails
                .Where(x => x.EmailStatus == EmailStatus.Sent)
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
