using Lefish.Application.Commands.AttackEvents.RemoveAttackEvent;

namespace Lefish.Web.Pages.AttackEvents;

public class RemoveVisitModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveAttackEventCommand command) : UserTokenPageModel(userToken)
{
    public AttackEventPlus AttackEvent { get; set; }

    public Attack Attack { get; set; }

    [BindProperty]
    public RemoveAttackEventCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
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
                    TargetName = x.Attack.EmailTarget.Name,
                    TargetAddress = x.Attack.EmailTarget.Address,
                    EmailTargetId = x.Attack.EmailTargetId,
                    AttackId = x.AttackId,
                })
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Attack = await database.Attacks
                .Where(x => x.Id == AttackEvent.AttackId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveAttackEventCommandModel()
            {
                VisitId = AttackEvent.Id,
            };

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

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            AttackEvent = await database.AttackEvents
                .Where(x => x.Id == CommandModel.VisitId)
                .Select(x => new AttackEventPlus()
                {
                    Url = x.Url,
                    Method = x.Method,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    PageName = x.Attack.PayloadPage.Name,
                    PayloadPageId = x.Attack.PayloadPageId,
                    TargetName = x.Attack.EmailTarget.Name,
                    TargetAddress = x.Attack.EmailTarget.Address,
                    EmailTargetId = x.Attack.EmailTargetId,
                    AttackId = x.AttackId,
                })
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Attack = await database.Attacks
                .Where(x => x.Id == AttackEvent.AttackId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-attack/{Attack.Id}");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
