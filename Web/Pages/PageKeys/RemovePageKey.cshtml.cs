using Lefish.Application.Commands.PageKeys.RemovePageKey;

namespace Lefish.Web.Pages.PageKeys;

public class RemovePageKeyModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemovePageKeyCommand command) : UserTokenPageModel(userToken)
{
    public PageKey PageKey { get; set; }

    [BindProperty]
    public RemovePageKeyCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            PageKey = await database.PageKeys
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemovePageKeyCommandModel()
            {
                PageKeyId = PageKey.Id,
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

            PageKey = await database.PageKeys
                .Where(x => x.Id == CommandModel.PageKeyId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            var emailTargetId = PageKey.EmailTargetId;

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-email-target/{emailTargetId}");
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
