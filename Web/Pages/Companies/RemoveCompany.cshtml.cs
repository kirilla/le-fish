using Lefish.Application.Commands.Companies.RemoveCompany;

namespace Lefish.Web.Pages.Companies;

public class RemoveCompanyModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveCompanyCommand command) : UserTokenPageModel(userToken)
{
    public Company Company { get; set; }

    [BindProperty]
    public RemoveCompanyCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Company = await database.Companies
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveCompanyCommandModel()
            {
                CompanyId = Company.Id,
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

            Company = await database.Companies
                .Where(x => x.Id == CommandModel.CompanyId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/companies");
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
