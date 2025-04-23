using Microsoft.AspNetCore.Authorization;

namespace Lefish.Web.Pages;

[AllowAnonymous]
public class IndexModel(IUserToken userToken) : UserTokenPageModel(userToken)
{
    public IActionResult OnGet()
    {
        try
        {
            if (UserToken.IsAuthenticated)
                return Redirect("/desktop");

            return Redirect("/frontpage");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
