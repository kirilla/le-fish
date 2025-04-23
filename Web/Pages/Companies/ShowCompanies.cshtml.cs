using Lefish.Application.Commands.Companies.AddCompany;

namespace Lefish.Web.Pages.Companies;

public class ShowCompaniesModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddCompanyCommand addCompanyCommand) : UserTokenPageModel(userToken)
{
    public List<Company> Companies { get; set; }

    public bool CanAddCompany { get; set; }
        = addCompanyCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            Companies = await database.Companies
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Created)
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
