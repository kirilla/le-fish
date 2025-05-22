using Lefish.Application.Commands.ScriptVisits.RemoveScriptVisit;

namespace Lefish.Web.Pages.ScriptVisits;

public class RemoveScriptVisitModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveScriptVisitCommand command) : UserTokenPageModel(userToken)
{
    public ScriptVisitPlus ScriptVisit { get; set; }

    [BindProperty]
    public RemoveScriptVisitCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            ScriptVisit = await database.ScriptVisits
                .Where(x => x.Id == id)
                .Select(x => new ScriptVisitPlus()
                {
                    Url = x.Url,
                    Method = x.Method,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    PageName = x.PageToken.PayloadPage.Name,
                    PayloadPageId = x.PageToken.PayloadPageId,
                    TargetName = x.PageToken.EmailMessage.EmailTarget.Name,
                    TargetAddress = x.PageToken.EmailMessage.EmailTarget.Address,
                    EmailTargetId = x.PageToken.EmailMessageId,
                })
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveScriptVisitCommandModel()
            {
                ScriptVisitId = ScriptVisit.Id,
            };

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

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            ScriptVisit = await database.ScriptVisits
                .Where(x => x.Id == CommandModel.ScriptVisitId)
                .Select(x => new ScriptVisitPlus()
                {
                    Url = x.Url,
                    Method = x.Method,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    PageName = x.PageToken.PayloadPage.Name,
                    PayloadPageId = x.PageToken.PayloadPageId,
                    TargetName = x.PageToken.EmailMessage.EmailTarget.Name,
                    TargetAddress = x.PageToken.EmailMessage.EmailTarget.Address,
                    EmailTargetId = x.PageToken.EmailMessageId,
                })
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-script-visits");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
