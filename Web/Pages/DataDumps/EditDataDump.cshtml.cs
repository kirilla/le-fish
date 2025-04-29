using Lefish.Application.Commands.DataDumps.EditDataDump;

namespace Lefish.Web.Pages.DataDumps;

public class EditDataDumpModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditDataDumpCommand command) : UserTokenPageModel(userToken)
{
    public DataDump DataDump { get; set; }

    [BindProperty]
    public EditDataDumpCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            DataDump = await database.DataDumps
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditDataDumpCommandModel()
            {
                DataDumpId = DataDump.Id,
                Name = DataDump.Name,
                ContentType = DataDump.ContentType,
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

            DataDump = await database.DataDumps
                .Where(x => x.Id == CommandModel.DataDumpId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-data-dump/{id}");
        }
        catch (BlockedByNameException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns en annat datadump med samma namn.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
