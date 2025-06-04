using Lefish.Application.Extensions;
using Lefish.Common.Dates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text;

namespace Lefish.Web.Pages;

[IgnoreAntiforgeryToken]
[AllowAnonymous]
public class FetchInstructionModel(
    IUserToken userToken,
    IDatabaseService database,
    IDateService dateService,
    IOptions<TemplateConfiguration> templateConfiguration) : UserTokenPageModel(userToken)
{
    private readonly TemplateConfiguration _config = templateConfiguration.Value;

    public async Task<IActionResult> OnGetAsync(int token)
    {
        try
        {
            var attack = await database.Attacks
                .AsNoTracking()
                .Where(x => x.Value == token)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            var target = await database.EmailTargets
                .AsNoTracking()
                .Where(x => x.Id == attack.EmailTargetId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            var instruction = await database.TargetInstructions
                .Where(x => 
                    x.AttackId == attack.Id && 
                    x.InstructionStatus == InstructionStatus.Waiting)
                .OrderBy(x => x.Created)
                .FirstOrDefaultAsync() ??
                throw new NotFoundException();

            if (instruction == null)
                return NotFound();

            instruction.InstructionStatus = InstructionStatus.Fetched;
            instruction.Fetched = dateService.GetDateTimeNow();

            await LogEvent(HttpContext, attack);

            await database.SaveAsync(UserToken);

            var script = instruction.ReplaceVariables(_config, target, attack);

            return Content(script, "application/javascript", Encoding.UTF8);
        }
        catch
        {
            return NotFound();
        }
    }

    public async Task LogEvent(
        HttpContext context, Attack attack)
    {
        var visit = new Visit()
        {
            VisitKind = VisitKind.FetchInstruction,
            Url = context.Request.GetDisplayUrl(),
            Method = context.Request.Method,
            IpAddress = context.Connection.RemoteIpAddress?.ToString(),
            UserAgent = context.Request.Headers?.UserAgent,
            AttackId = attack.Id,
        };

        database.Visits.Add(visit);

        //await database.SaveAsync(UserToken);
    }
}
