using Lefish.Application.Commands.PayloadScripts.ClonePayloadScript;
using Lefish.Application.Commands.PayloadScripts.EditPayloadScript;
using Lefish.Application.Commands.PayloadScripts.RemovePayloadScript;

namespace Lefish.Web.Pages.PayloadScripts;

public class ShowPayloadScriptModel(
    IUserToken userToken,
    IDatabaseService database,
    IClonePayloadScriptCommand clonePayloadScriptCommand,
    IEditPayloadScriptCommand editPayloadScriptCommand,
    IRemovePayloadScriptCommand removePayloadScriptCommand,
    IOptions<TemplateConfiguration> templateConfiguration) : UserTokenPageModel(userToken)
{
    public readonly TemplateConfiguration Config = templateConfiguration.Value;

    public PayloadScript PayloadScript { get; set; }

    public List<PageKeyPlus> PageTokens { get; set; }

    public bool CanClonePayloadScript { get; set; }
        = clonePayloadScriptCommand.IsPermitted(userToken);

    public bool CanEditPayloadScript { get; set; }
        = editPayloadScriptCommand.IsPermitted(userToken);

    public bool CanRemovePayloadScript { get; set; }
        = removePayloadScriptCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            PayloadScript = await database.PayloadScripts
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            PageTokens = await database.PageKeys
                .Where(x => x.PayloadScriptId == id)
                .OrderBy(x => x.EmailMessage.EmailTarget.Name)
                .ThenBy(x => x.EmailMessage.EmailTarget.Address)
                .Select(x => new PageKeyPlus()
                {
                    Id = x.Id,
                    Token = x.Token,
                    Created = x.Created,
                    PageName = x.PayloadScript.Name,
                    PayloadPageId = x.PayloadPageId,
                    PayloadScriptId = x.PayloadScriptId,
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
