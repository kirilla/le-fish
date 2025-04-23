using Lefish.Application.Commands.EmailImages.EditEmailImage;
using Lefish.Application.Commands.EmailImages.RemoveEmailImage;

namespace Lefish.Web.Pages.EmailImages;

public class ShowEmailImageModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditEmailImageCommand editEmailImageCommand,
    IRemoveEmailImageCommand removeEmailImageCommand) : UserTokenPageModel(userToken)
{
    public EmailImage EmailImage { get; set; }

    public bool CanEditEmailImage { get; set; }
        = editEmailImageCommand.IsPermitted(userToken);

    public bool CanRemoveEmailImage { get; set; }
        = removeEmailImageCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            EmailImage = await database.EmailImages
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

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
