using Microsoft.AspNetCore.Authorization;

namespace Lefish.Web.Pages.Help;

[AllowAnonymous]
public class NotFoundModel(IUserToken userToken) : UserTokenPageModel(userToken)
{
    public void OnGet()
    {
    }
}
