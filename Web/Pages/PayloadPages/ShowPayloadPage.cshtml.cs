using Lefish.Application.Commands.PayloadPages.ClonePayloadPage;
using Lefish.Application.Commands.PayloadPages.EditPayloadPage;
using Lefish.Application.Commands.PayloadPages.RemovePayloadPage;

namespace Lefish.Web.Pages.PayloadPages;

public class ShowPayloadPageModel(
    IUserToken userToken,
    IDatabaseService database,
    IClonePayloadPageCommand clonePayloadPageCommand,
    IEditPayloadPageCommand editPayloadPageCommand,
    IRemovePayloadPageCommand removePayloadPageCommand,
    IOptions<TemplateConfiguration> templateConfiguration) : UserTokenPageModel(userToken)
{
    public readonly TemplateConfiguration Config = templateConfiguration.Value;

    public PayloadPage PayloadPage { get; set; }

    public List<PageKeyPlus> PageTokens { get; set; }

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

            PageTokens = await database.PageKeys
                .Where(x => x.PayloadPageId == id)
                .OrderBy(x => x.EmailMessage.EmailTarget.Name)
                .ThenBy(x => x.EmailMessage.EmailTarget.Address)
                .Select(x => new PageKeyPlus()
                {
                    Id = x.Id,
                    Token = x.Token,
                    Created = x.Created,
                    PageName = x.PayloadPage.Name,
                    PayloadPageId = x.PayloadPageId,
                    TargetName = x.EmailMessage.EmailTarget.Name,
                    TargetAddress = x.EmailMessage.EmailTarget.Address,
                    EmailTargetId = x.EmailMessage.EmailTargetId,
                    PageVisitCount = x.PageVisits.Count(),
                })
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
