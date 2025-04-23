using Lefish.Application.Commands.EmailTemplates.AddEmailTemplate;

namespace Lefish.Web.Pages.EmailTemplates;

public class AddEmailTemplateModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddEmailTemplateCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public AddEmailTemplateCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddEmailTemplateCommandModel();

            return Page();
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

            if (!ModelState.IsValid)
                return Page();

            var id = await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-email-template/{id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
