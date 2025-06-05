using Lefish.Application.Commands.AttackEvents.RemoveAttackEvent;

namespace Lefish.Web.Pages.AttackEvents;

public class ShowAttackEventModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveAttackEventCommand removeAttackEventCommand) : UserTokenPageModel(userToken)
{
    public AttackEventPlus AttackEvent { get; set; }

    public bool CanRemoveAttackEvent { get; set; }
        = removeAttackEventCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AttackEvent = await database.AttackEvents
                .Where(x => x.Id == id)
                .Select(x => new AttackEventPlus()
                {
                    Id = x.Id,
                    Url = x.Url,
                    Method = x.Method,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    PageName = x.Attack.PayloadPage.Name,
                    PayloadPageId = x.Attack.PayloadPageId,
                    TargetName = x.Attack.Target.Name,
                    TargetAddress = x.Attack.Target.Address,
                    TargetId = x.Attack.TargetId,
                })
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

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
