using Lefish.Application.Commands.Companies.EditCompany;

namespace Lefish.Web.Pages.Companies;

public class EditCompanyModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditCompanyCommand command) : UserTokenPageModel(userToken)
{
    public Company Company { get; set; }

    [BindProperty]
    public EditCompanyCommandModel CommandModel { get; set; }

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

            CommandModel = new EditCompanyCommandModel()
            {
                CompanyId = Company.Id,
                Name = Company.Name,
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

            Company = await database.Companies
                .Where(x => x.Id == CommandModel.CompanyId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-company/{id}");
        }
        catch (BlockedByNameException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns ett annat företag med samma namn.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
