using Lefish.Application.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Text;

namespace Lefish.Web.Pages;

[IgnoreAntiforgeryToken]
[AllowAnonymous]
public class FetchInstructionModel(
    IUserToken userToken,
    IDatabaseService database,
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

            await database.SaveAsync(UserToken);

            var script = instruction.ReplaceVariables(_config, target, attack);

            return Content(script, "application/javascript", Encoding.UTF8);
        }
        catch
        {
            return NotFound();
        }
    }
}
