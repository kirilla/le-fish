using Lefish.Application.Commands.PageVisits.RemovePageVisit;

namespace Lefish.Web.Pages.PageVisits;

public class RemovePageVisitModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemovePageVisitCommand command) : UserTokenPageModel(userToken)
{
    public PageVisitPlus PageVisit { get; set; }

    public PageKey PageKey { get; set; }

    [BindProperty]
    public RemovePageVisitCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            PageVisit = await database.PageVisits
                .Where(x => x.Id == id)
                .Select(x => new PageVisitPlus()
                {
                    Id = x.Id,
                    Url = x.Url,
                    Method = x.Method,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    PageName = x.PageKey.PayloadPage.Name,
                    PayloadPageId = x.PageKey.PayloadPageId,
                    TargetName = x.PageKey.EmailTarget.Name,
                    TargetAddress = x.PageKey.EmailTarget.Address,
                    EmailTargetId = x.PageKey.EmailTargetId,
                    PageKeyId = x.PageKeyId,
                })
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            PageKey = await database.PageKeys
                .Where(x => x.Id == PageVisit.PageKeyId)
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
                .Where(x => x.Id == CommandModel.PageVisitId)
                .Select(x => new PageVisitPlus()
                {
                    Url = x.Url,
                    Method = x.Method,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    PageName = x.PageKey.PayloadPage.Name,
                    PayloadPageId = x.PageKey.PayloadPageId,
                    TargetName = x.PageKey.EmailTarget.Name,
                    TargetAddress = x.PageKey.EmailTarget.Address,
                    EmailTargetId = x.PageKey.EmailTargetId,
                    PageKeyId = x.PageKeyId,
                })
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            PageKey = await database.PageKeys
                .Where(x => x.Id == PageVisit.PageKeyId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-page-key/{PageKey.Id}");
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
