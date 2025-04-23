using Lefish.Application.Commands.Sessions.SignIn;
using Lefish.Common.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Lefish.Web.Pages.Sessions;

[AllowAnonymous]
public class SignInModel(
    ISignInCommand signInCommand,
    IOptions<UserAccountConfiguration> userAccountOptions,
    IUserToken userToken) : UserTokenPageModel(userToken)
{
    private readonly UserAccountConfiguration _config = userAccountOptions.Value;

    [BindProperty]
    public SignInCommandModel CommandModel { get; set; }

    public IActionResult OnGet()
    {
        try
        {
            if (!_config.SignInAllowed)
                throw new FeatureTurnedOffException();

            if (UserToken.IsAuthenticated)
                throw new AlreadyLoggedInException();

            return Page();
        }
        catch (AlreadyLoggedInException)
        {
            return Redirect("/help/already-logged-in");
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
            if (!_config.SignInAllowed)
                throw new FeatureTurnedOffException();

            if (UserToken.IsAuthenticated)
                throw new AlreadyLoggedInException();

            Thread.Sleep(
                TimeSpan.FromMilliseconds(
                    Random.Shared.Next(200, 600)));

            if (!ModelState.IsValid)
                return Page();

            var loginResult = await signInCommand.Execute(UserToken, CommandModel);

            var claims = new List<Claim>
            {
                new Claim("UserGuid", loginResult.UserGuid.ToString()),
                new Claim("SessionGuid", loginResult.SessionGuid.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return Redirect("/desktop");
        }
        catch (AlreadyLoggedInException)
        {
            return Redirect("/help/already-logged-in");
        }
        catch (FeatureTurnedOffException)
        {
            return Redirect("/help/featureturnedoff");
        }
        catch (Exception ex)
        {
            if (ex is UserNotFoundException ||
                ex is PasswordVerificationFailedException)
            {
                // NOTE: Don't let the user/attacker know
                // which of these is the case.

                ModelState.AddModelError(
                    nameof(CommandModel.EmailAddress), "Felaktigt lösenord.");

                return Page();
            }

            return Redirect("/help/notpermitted");
        }
    }
}
