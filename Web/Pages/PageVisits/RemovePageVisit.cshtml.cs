using Lefish.Application.Commands.PageVisits.RemovePageVisit;

namespace Lefish.Web.Pages.PageVisits;

public class RemovePageVisitModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemovePageVisitCommand command) : UserTokenPageModel(userToken)
{
    public PageVisitPlus PageVisit { get; set; }

    public Attack Attack { get; set; }

    [BindProperty]
    public RemovePageVisitCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            PageVisit = await database.Visits
                .Where(x => x.Id == id)
                .Select(x => new PageVisitPlus()
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
                .Where(x => x.Id == PageVisit.AttackId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemovePageVisitCommandModel()
            {
                PageVisitId = PageVisit.Id,
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

            PageVisit = await database.Visits
                .Where(x => x.Id == CommandModel.PageVisitId)
                .Select(x => new PageVisitPlus()
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
                .Where(x => x.Id == PageVisit.AttackId)
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
