using Lefish.Application.Commands.PayloadPages.ClonePayloadPage;
using Lefish.Application.Commands.PayloadPages.EditPayloadPage;
using Lefish.Application.Commands.PayloadPages.RemovePayloadPage;

namespace Lefish.Web.Pages.PayloadPages;

public class ShowPayloadPageModel(
    IUserToken userToken,
    IDatabaseService database,
    IClonePayloadPageCommand clonePayloadPageCommand,
    IEditPayloadPageCommand editPayloadPageCommand,
    IRemovePayloadPageCommand removePayloadPageCommand) : UserTokenPageModel(userToken)
{
    public PayloadPage PayloadPage { get; set; }

    public List<PhishingToken> PhishingTokens { get; set; }

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

            PayloadPage = await database.PayloadPages
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            PhishingTokens = await database.PhishingTokens
                .Include(x => x.EmailMessage.EmailTarget)
                .Where(x => x.PayloadPageId == id)
                .OrderBy(x => x.EmailMessage.EmailTarget.Name)
                .ThenBy(x => x.EmailMessage.EmailTarget.Address)
                .ToListAsync();

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
