namespace Lefish.Web.Pages;

public class OutlineModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public List<EmailTarget> EmailTargets { get; set; }
    public List<EmailHeader> EmailMessages { get; set; }
    public List<PageVisitPlus> PageVisits { get; set; }
    public List<ScriptVisitPlus> ScriptVisits { get; set; }
    public List<PageTokenPlus> PageTokens { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            EmailTargets = await database.EmailTargets
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Address)
                .ToListAsync();

            EmailMessages = await database.EmailMessages
                .Include(x => x.EmailAccount)
                .Include(x => x.EmailTarget)
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

            PageVisits = await database.PageVisits
                .OrderBy(x => x.Created)
                .Select(x => new PageVisitPlus() { 
                    Id = x.Id,
                    PageTokenId = x.PageTokenId,
                    Created = x.Created,
                    //Url = x.Url,
                    //Method = x.Method,
                    IpAddress = x.IpAddress,
                    //UserAgent = x.UserAgent,
                    PageName = x.PageToken.PayloadPage.Name,
                    PayloadPageId = x.PageToken.PayloadPageId,
                    //TargetName = x.PageToken.EmailMessage.EmailTarget.Name,
                    //TargetAddress = x.PageToken.EmailMessage.EmailTarget.Address,
                    //EmailTargetId = x.PageToken.EmailMessage.EmailTargetId,
                })
                .ToListAsync();

            ScriptVisits = await database.ScriptVisits
                .OrderBy(x => x.Created)
                .Select(x => new ScriptVisitPlus()
                {
                    Id = x.Id,
                    Created = x.Created,
                    //Url = x.Url,
                    //Method = x.Method,
                    //IpAddress = x.IpAddress,
                    //UserAgent = x.UserAgent,
                    ScriptName = x.PageToken.PayloadScript.Name,
                    PayloadScriptId = x.PageToken.PayloadScriptId,
                    //TargetName = x.PageToken.EmailMessage.EmailTarget.Name,
                    //TargetAddress = x.PageToken.EmailMessage.EmailTarget.Address,
                    //EmailTargetId = x.PageToken.EmailMessage.EmailTargetId,
                })
                .ToListAsync();

            PageTokens = await database.PageTokens
                .OrderBy(x => x.Created)
                .Select(x => new PageTokenPlus()
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
