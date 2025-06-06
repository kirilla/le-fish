using Lefish.Application.Commands.PayloadPages.ClonePayloadPage;

namespace Lefish.Web.Pages.PayloadPages;

public class ClonePayloadPageModel(
    IUserToken userToken,
    IDatabaseService database,
    IClonePayloadPageCommand command) : UserTokenPageModel(userToken)
{
    public PayloadPage PayloadPage { get; set; }

    [BindProperty]
    public ClonePayloadPageCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            PayloadPage = await database.WebPages
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new ClonePayloadPageCommandModel()
            {
                PayloadPageId = PayloadPage.Id,
                Name = PayloadPage.Name,
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

    public async Task<IActionResult> OnPostAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            PayloadPage = await database.WebPages
                .Where(x => x.Id == CommandModel.PayloadPageId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            var cloneId = await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-payload-page/{cloneId}");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns en sida med samma namn.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
