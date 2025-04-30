using Lefish.Application.Commands.PageVisits.RemovePageVisit;

namespace Lefish.Web.Pages.PageVisits;

public class RemovePageVisitModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemovePageVisitCommand command) : UserTokenPageModel(userToken)
{
    public PageVisit PageVisit { get; set; }

    [BindProperty]
    public RemovePageVisitCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            PageVisit = await database.PageVisits
                .Include(x => x.EmailTarget)
                .Include(x => x.PayloadPage)
                .Where(x => x.Id == id)
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

            PageVisit = await database.PageVisits
                .Include(x => x.EmailTarget)
                .Include(x => x.PayloadPage)
                .Where(x => x.Id == CommandModel.PageVisitId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-page-visits");
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
