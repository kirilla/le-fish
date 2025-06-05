using Lefish.Application.Commands.EmailMessages.SendEmailToTarget;
using Lefish.Application.Commands.EmailTargets.EditEmailTarget;
using Lefish.Application.Commands.EmailTargets.RemoveEmailTarget;

namespace Lefish.Web.Pages.EmailTargets;

public class ShowEmailTargetModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditEmailTargetCommand editTargetCommand,
    IRemoveEmailTargetCommand removeTargetCommand,
    ISendEmailToTargetCommand sendEmailToTargetCommand) : UserTokenPageModel(userToken)
{
    public EmailTarget EmailTarget { get; set; }

    public List<Attack> Attacks { get; set; }

    public bool CanEditTarget { get; set; }
        = editTargetCommand.IsPermitted(userToken);

    public bool CanRemoveTarget { get; set; }
        = removeTargetCommand.IsPermitted(userToken);

    public bool CanSendEmailToTarget { get; set; }
        = sendEmailToTargetCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            EmailTarget = await database.Targets
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Attacks = await database.Attacks
                .Where(x => x.EmailTargetId == id)
                .OrderBy(x => x.Created)
                .ToListAsync();

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
