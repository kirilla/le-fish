using Lefish.Application.Commands.EmailTemplates.EditEmailTemplate;

namespace Lefish.Web.Pages.EmailTemplates;

public class EditEmailTemplateModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditEmailTemplateCommand command) : UserTokenPageModel(userToken)
{
    public EmailTemplate EmailTemplate { get; set; }

    [BindProperty]
    public EditEmailTemplateCommandModel CommandModel { get; set; }

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

            CommandModel = new EditEmailTemplateCommandModel()
            {
                EmailTemplateId = EmailTemplate.Id,
                Subject = EmailTemplate.Subject,
                HtmlBody = EmailTemplate.HtmlBody,
                TextBody = EmailTemplate.TextBody,
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

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-email-template/{id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
