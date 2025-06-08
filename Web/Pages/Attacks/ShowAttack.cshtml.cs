namespace Lefish.Web.Pages.Attacks;

public class ShowAttackModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public AttackSummary Attack { get; set; }
    public Target Target { get; set; }

    public List<DataResult> DataResults { get; set; }
    public List<EmailHeader> EmailHeaders { get; set; }
    public List<TargetInstructionSummary> TargetInstructions { get; set; }

    public int AttackEventCount { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            Attack = await database.Attacks
                .Where(x => x.Id == id)
                .Select(x => new AttackSummary()
                {
                    Id = x.Id,
                    Value = x.Value,
                    Created = x.Created,
                    PageName = x.WebPage.Name,
                    PayloadPageId = x.WebPageId,
                    TargetName = x.Target.Name,
                    TargetAddress = x.Target.Address,
                    TargetId = x.TargetId,
                    ScriptName = x.PageScript.Name,
                    PageScriptId = x.PageScriptId,
                })
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Target = await database.Targets
                .Where(x => x.Id == Attack.TargetId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            DataResults = await database.DataResults
                .Where(x => x.AttackId == id)
                .OrderBy(x => x.Created)
                .ToListAsync();

            EmailHeaders = await database.EmailMessages
                .Where(x => x.AttackId == id)
                .OrderBy(x => x.Created)
                .Select(x => new EmailHeader()
                {
                    Id = x.Id,
                    AttackId = x.AttackId,
                    ToName = x.ToName,
                    ToAddress = x.ToAddress,
                    FromName = x.EmailAccount!.FromName,
                    FromAddress = x.EmailAccount!.FromAddress,
                    ReplyToName = x.EmailAccount.ReplyToName,
                    ReplyToAddress = x.EmailAccount.ReplyToAddress,
                    Subject = x.Subject,
                    EmailStatus = x.EmailStatus,
                    Created = x.Created,
                    Sent = x.Sent,
                })
                .ToListAsync();

            AttackEventCount = await database.AttackEvents
                .Where(x => x.AttackId == id)
                .CountAsync();

            TargetInstructions = await database.TargetInstructions
                .Where(x => x.AttackId == id)
                .OrderBy(x => x.Created)
                .Select(x => new TargetInstructionSummary()
                {
                    Id = x.Id,
                    Created = x.Created,
                    Fetched = x.Fetched,
                    Name = x.Name,
                    Reference = x.Reference,
                    InstructionStatus = x.InstructionStatus,
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
