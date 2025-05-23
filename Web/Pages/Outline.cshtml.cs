using System.ComponentModel;
using System.Linq;

namespace Lefish.Web.Pages;

public class OutlineModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public List<EmailTarget> EmailTargets { get; set; }
    public List<EmailHeader> EmailMessages { get; set; }
    public List<PageTokenPlus> PageTokens { get; set; }
    public List<Visit> Visits { get; set; }

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

            var pageVisits = await database.PageVisits
                .OrderBy(x => x.Created)
                .Select(x => new Visit() { 
                    VisitKind = VisitKind.Page,
                    Id = x.Id,
                    PageTokenId = x.PageTokenId,
                    Created = x.Created,
                    IpAddress = x.IpAddress,
                    PageName = x.PageToken.PayloadPage.Name,
                    PayloadPageId = x.PageToken.PayloadPageId,
                })
                .ToListAsync();

            var scriptVisits = await database.ScriptVisits
                .OrderBy(x => x.Created)
                .Select(x => new Visit()
                {
                    VisitKind = VisitKind.Script,
                    Id = x.Id,
                    PageTokenId = x.PageTokenId,
                    Created = x.Created,
                    IpAddress = x.IpAddress,
                    ScriptName = x.PageToken.PayloadScript.Name,
                    PayloadScriptId = x.PageToken.PayloadScriptId,
                })
                .ToListAsync();

            Visits = pageVisits
                .Union(scriptVisits)
                .OrderBy(x => x.Created)
                .ToList();

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
