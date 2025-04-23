using Microsoft.AspNetCore.Authorization;

namespace Lefish.Web.Pages.Sessions;

[AllowAnonymous]
public class SignOutSuccessModel(IUserToken userToken) : UserTokenPageModel(userToken)
{
    public void OnGet()
    {
    }
}
