using Lefish.Application.Commands.DataDumps.EditDataDump;

namespace Lefish.Web.Pages.DataDumps;

public class EditDataDumpModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditDataDumpCommand command) : UserTokenPageModel(userToken)
{
    public DataResult DataDump { get; set; }

    [BindProperty]
    public EditDataDumpCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            DataDump = await database.DataResults
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditDataDumpCommandModel()
            {
                Id = DataDump.Id,
                JsonData = DataDump.JsonData,
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

            DataDump = await database.DataResults
                .Where(x => x.Id == CommandModel.Id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-data-dump/{id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
