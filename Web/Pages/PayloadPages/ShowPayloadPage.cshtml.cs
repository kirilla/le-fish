using Lefish.Application.Commands.PayloadPages.CloneWebPage;
using Lefish.Application.Commands.PayloadPages.EditWebPage;
using Lefish.Application.Commands.PayloadPages.RemovePayloadPage;

namespace Lefish.Web.Pages.PayloadPages;

public class ShowPayloadPageModel(
    IUserToken userToken,
    IDatabaseService database,
    ICloneWebPageCommand clonePayloadPageCommand,
    IEditWebPageCommand editPayloadPageCommand,
    IRemoveWebPageCommand removePayloadPageCommand,
    IOptions<TemplateConfiguration> templateConfiguration) : UserTokenPageModel(userToken)
{
    public readonly TemplateConfiguration Config = templateConfiguration.Value;

    public WebPage PayloadPage { get; set; }

    public bool CanClonePayloadPage { get; set; }
        = clonePayloadPageCommand.IsPermitted(userToken);

    public bool CanEditPayloadPage { get; set; }
        = editPayloadPageCommand.IsPermitted(userToken);

    public bool CanRemovePayloadPage { get; set; }
        = removePayloadPageCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            PayloadPage = await database.WebPages
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
