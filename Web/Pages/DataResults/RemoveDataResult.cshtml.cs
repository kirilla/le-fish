using Lefish.Application.Commands.DataResults.RemoveDataResult;

namespace Lefish.Web.Pages.DataResults;

public class RemoveDataResultModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveDataResultCommand command) : UserTokenPageModel(userToken)
{
    public DataResult DataResult { get; set; }

    [BindProperty]
    public RemoveDataResultCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            DataResult = await database.DataResults
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveDataResultCommandModel()
            {
                Id = DataResult.Id,
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

            DataResult = await database.DataResults
                .Where(x => x.Id == CommandModel.Id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-data-results");
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
