using Lefish.Application.Commands.Companies.EditCompany;
using Lefish.Application.Commands.Companies.RemoveCompany;

namespace Lefish.Web.Pages.Companies;

public class ShowCompanyModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditCompanyCommand editCompanyCommand,
    IRemoveCompanyCommand removeCompanyCommand) : UserTokenPageModel(userToken)
{
    public Company Company { get; set; }

    public bool CanEditCompany { get; set; }
        = editCompanyCommand.IsPermitted(userToken);

    public bool CanRemoveCompany { get; set; }
        = removeCompanyCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            Company = await database.Companies
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

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
