using Lefish.Application.Commands.IpRanges.AddIpRange;

namespace Lefish.Web.Pages.IpRanges;

public class AddIpRangeModel(
    IIpRangeCacheService cacheService,
    IUserToken userToken,
    IDatabaseService database,
    IAddIpRangeCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public AddIpRangeCommandModel CommandModel { get; set; }

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddIpRangeCommandModel();

            return Page();
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
        catch (FormatException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Range),
                "Oväntat format.");

            return Page();
        }
        catch (MissingPartException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Range),
                "Det saknas något i adressen.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
