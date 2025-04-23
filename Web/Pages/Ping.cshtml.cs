using Microsoft.AspNetCore.Authorization;

namespace Lefish.Web.Pages;

[AllowAnonymous]
public class PingModel : PageModel
{
    public PingModel()
    {
    }

    public void OnGet()
    {
    }
}
