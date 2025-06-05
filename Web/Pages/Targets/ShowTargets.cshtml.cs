using Lefish.Application.Commands.Targets.AddTarget;

namespace Lefish.Web.Pages.Targets;

public class ShowTargetsModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddTargetCommand addTargetCommand) : UserTokenPageModel(userToken)
{
    public List<Target> Targets { get; set; }

    public bool CanAddTarget { get; set; }
        = addTargetCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            Targets = await database.Targets
                .AsNoTracking()
                .OrderBy(x => x.Address)
                .ThenBy(x => x.Name)
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
