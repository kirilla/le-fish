namespace Lefish.Web.Pages.EmailMessages;

public class ShowEmailQueueModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public List<EmailHeader> EmailHeaders { get; set; }

    public int NotSent { get; set; }
    public int Failed { get; set; }
    public int Sent { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            EmailHeaders = await database.EmailMessages
                .Include(x => x.EmailAccount)
                .OrderBy(x => x.Created)
                .Select(x => new EmailHeader()
                {
                    Id = x.Id,
                    ToName = x.ToName,
                    ToAddress = x.ToAddress,
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

            NotSent = EmailHeaders.Count(x => x.EmailStatus == EmailStatus.NotSent);
            Failed = EmailHeaders.Count(x => x.EmailStatus == EmailStatus.SendFailed);
            Sent = EmailHeaders.Count(x => x.EmailStatus == EmailStatus.Sent);

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
