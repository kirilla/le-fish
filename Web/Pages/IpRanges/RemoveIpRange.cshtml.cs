using Lefish.Application.Commands.IpRanges.RemoveIpRange;

namespace Lefish.Web.Pages.IpRanges;

public class RemoveIpRangeModel(
    IIpRangeCacheService cacheService,
    IUserToken userToken,
    IDatabaseService database,
    IRemoveIpRangeCommand command) : UserTokenPageModel(userToken)
{
    public IpRange IpRange { get; set; }

    [BindProperty]
    public RemoveIpRangeCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            IpRange = await database.IpRanges
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveIpRangeCommandModel()
            {
                Id = IpRange.Id,
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

            IpRange = await database.IpRanges
                .Where(x => x.Id == CommandModel.Id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            cacheService.InvalidateCache();

            return Redirect("/show-ip-ranges");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort adressen.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
