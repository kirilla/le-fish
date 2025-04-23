using Lefish.Application.Commands.Account.RegisterAccount;
using Lefish.Common.Settings;
using Microsoft.AspNetCore.Authorization;

namespace Lefish.Web.Pages.Account;

[AllowAnonymous]
public class RegisterAccountModel(
    IUserToken userToken,
    IRegisterAccountCommand command,
    IOptions<UserAccountConfiguration> options) : UserTokenPageModel(userToken)
{
    private readonly UserAccountConfiguration _config = options.Value;

    [BindProperty]
    public RegisterAccountCommandModel CommandModel { get; set; }

    public IActionResult OnGet()
    {
        try
        {
            if (!_config.RegisterAccountAllowed)
                throw new FeatureTurnedOffException();

            if (!command.IsPermitted(userToken))
                throw new NotPermittedException();

            CommandModel = new RegisterAccountCommandModel();

            return Page();
        }
        catch (FeatureTurnedOffException)
        {
            return Redirect("/help/featureturnedoff");
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
            if (!_config.RegisterAccountAllowed)
                throw new FeatureTurnedOffException();

            if (!command.IsPermitted(userToken))
                throw new NotPermittedException();
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await command.Execute(UserToken, CommandModel);

            return Redirect("/register-account-success");
        }
        catch (EmailAlreadyTakenException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.EmailAddress),
                "Epostadressen används redan av en annan användare.");

            return Page();
        }
        catch (FeatureTurnedOffException)
        {
            return Redirect("/help/featureturnedoff");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
