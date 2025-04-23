using Microsoft.AspNetCore.Authorization;

namespace Lefish.Web.Pages;

[AllowAnonymous]
public class FrontpageModel(IUserToken userToken) : UserTokenPageModel(userToken)
{
    public IActionResult OnGet()
    {
        try
        {
            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
