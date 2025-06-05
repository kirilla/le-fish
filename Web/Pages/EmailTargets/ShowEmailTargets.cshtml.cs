using Lefish.Application.Commands.EmailTargets.AddEmailTarget;

namespace Lefish.Web.Pages.EmailTargets;

public class ShowEmailTargetsModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddEmailTargetCommand addTargetCommand) : UserTokenPageModel(userToken)
{
    public List<EmailTarget> EmailTargets { get; set; }

    public bool CanAddTarget { get; set; }
        = addTargetCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            EmailTargets = await database.Targets
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
