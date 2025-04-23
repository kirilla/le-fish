using Lefish.Application.Commands.EmailTemplates.CloneEmailTemplate;

namespace Lefish.Web.Pages.EmailTemplates;

public class CloneEmailTemplateModel(
    IUserToken userToken,
    IDatabaseService database,
    ICloneEmailTemplateCommand command) : UserTokenPageModel(userToken)
{
    public EmailTemplate EmailTemplate { get; set; }

    [BindProperty]
    public CloneEmailTemplateCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailTemplate = await database.EmailTemplates
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new CloneEmailTemplateCommandModel()
            {
                EmailTemplateId = EmailTemplate.Id,
                CloneSubject = EmailTemplate.Subject,
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

            EmailTemplate = await database.EmailTemplates
                .Where(x => x.Id == CommandModel.EmailTemplateId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            var cloneId = await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-email-template/{cloneId}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
