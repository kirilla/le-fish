using Microsoft.AspNetCore.Authorization;

namespace Lefish.Web.Pages.Account;

[AllowAnonymous]
public class DeleteAccountSuccessModel(
    IUserToken userToken) : UserTokenPageModel(userToken)
{
    public void OnGet()
    {
    }
}
