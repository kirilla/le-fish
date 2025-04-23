namespace Lefish.Web.Common;

public class UserTokenPageModel(IUserToken userToken) : PageModel
{
    public IUserToken UserToken { get; } = userToken;
}
