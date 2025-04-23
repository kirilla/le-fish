using Lefish.Application.Commands.EmailTemplates.AddEmailTemplate;

namespace Lefish.Web.Pages.EmailTemplates;

public class ShowEmailTemplatesModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddEmailTemplateCommand addTemplateCommand) : UserTokenPageModel(userToken)
{
    public List<EmailTemplate> EmailTemplates { get; set; }

    public bool CanAddTemplate { get; set; }
        = addTemplateCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            EmailTemplates = await database.EmailTemplates
                .AsNoTracking()
                .OrderBy(x => x.Subject)
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
