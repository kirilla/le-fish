using Lefish.Application.Commands.EmailMessages.SendEmail;

namespace Lefish.Web.Pages;

public class AttackModel(
    IUserToken userToken,
    IDatabaseService database,
    ISendEmailCommand sendEmailCommand) : UserTokenPageModel(userToken)
{
    public List<EmailTarget> EmailTargets { get; set; }
    public List<EmailHeader> EmailMessages { get; set; }
    public List<AttackSummary> Attacks { get; set; }
    public List<Visit> Visits { get; set; }

    public bool CanSendEmail { get; set; }
        = sendEmailCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            EmailTargets = await database.EmailTargets
                .Where(x => x.Attacks.Any())
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Address)
                .ToListAsync();

            EmailMessages = await database.EmailMessages
                .Include(x => x.EmailAccount)
                .OrderByDescending(x => x.Created)
                .Select(x => new EmailHeader()
                {
                    Id = x.Id,
                    PageKeyId = x.PageKeyId,
                    Subject = x.Subject,
                    EmailStatus = x.EmailStatus,
                    Sent = x.Sent,
                })
                .ToListAsync();

            var pageVisits = await database.PageVisits
                .OrderBy(x => x.Created)
                .Select(x => new Visit() { 
                    VisitKind = VisitKind.Page,
                    Id = x.Id,
                    PageKeyId = x.PageKeyId,
                    Created = x.Created,
                    IpAddress = x.IpAddress,
                    PageName = x.Attack.PayloadPage.Name,
                    PayloadPageId = x.Attack.PayloadPageId,
                })
                .ToListAsync();

            var scriptVisits = await database.ScriptVisits
                .OrderBy(x => x.Created)
                .Select(x => new Visit()
                {
                    VisitKind = VisitKind.Script,
                    Id = x.Id,
                    PageKeyId = x.PageKeyId,
                    Created = x.Created,
                    IpAddress = x.IpAddress,
                    ScriptName = x.Attack.PayloadScript.Name,
                    PayloadScriptId = x.Attack.PayloadScriptId,
                })
                .ToListAsync();

            Visits = pageVisits
                .Union(scriptVisits)
                .OrderBy(x => x.Created)
                .ToList();

            Attacks = await database.Attacks
                .OrderBy(x => x.Created)
                .Select(x => new AttackSummary()
                {
                    Id = x.Id,
                    Value = x.Value,
                    PageName = x.PayloadPage.Name,
                    PayloadPageId = x.PayloadPageId,
                    TargetName = x.EmailTarget.Name,
                    TargetAddress = x.EmailTarget.Address,
                    EmailTargetId = x.EmailTargetId,
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
