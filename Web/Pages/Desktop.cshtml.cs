namespace Lefish.Web.Pages;

public class DesktopModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public IActionResult OnGet()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
