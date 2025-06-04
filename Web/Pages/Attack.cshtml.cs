using Lefish.Application.Commands.EmailMessages.SendEmail;

namespace Lefish.Web.Pages;

public class AttackModel(
    IUserToken userToken,
    IDatabaseService database,
    ISendEmailCommand sendEmailCommand) : UserTokenPageModel(userToken)
{
    public List<EmailTarget> EmailTargets { get; set; }
    public List<Attack> Attacks { get; set; }

    public bool CanSendEmail { get; set; }
        = sendEmailCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            EmailTargets = await database.EmailTargets
                .Where(x => x.Attacks.Any())
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Address)
                .ToListAsync();

            Attacks = await database.Attacks
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
