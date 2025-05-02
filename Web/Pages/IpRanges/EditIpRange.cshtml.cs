using Lefish.Application.Commands.IpRanges.EditIpRange;

namespace Lefish.Web.Pages.IpRanges;

public class EditIpRangeModel(
    IIpRangeCacheService cacheService,
    IUserToken userToken,
    IDatabaseService database,
    IEditIpRangeCommand command) : UserTokenPageModel(userToken)
{
    public IpRange IpRange { get; set; }

    [BindProperty]
    public EditIpRangeCommandModel CommandModel { get; set; }

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

            CommandModel = new EditIpRangeCommandModel()
            {
                Id = IpRange.Id,
                Range = IpRange.Range,
                Blocked = IpRange.Blocked,
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
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Range),
                "Intervallet finns redan.");

            return Page();
        }
        catch (FormatException ex)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Range),
                "Ogiltig CIDR-adressblock.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
