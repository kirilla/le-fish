namespace Lefish.Web.Pages;

public class InfoModel(
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
