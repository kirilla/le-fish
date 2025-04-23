using Lefish.Application.Commands.EmailTemplates.RemoveEmailTemplate;

namespace Lefish.Web.Pages.EmailTemplates;

public class RemoveEmailTemplateModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveEmailTemplateCommand command) : UserTokenPageModel(userToken)
{
    public EmailTemplate EmailTemplate { get; set; }

    [BindProperty]
    public RemoveEmailTemplateCommandModel CommandModel { get; set; }

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

            CommandModel = new RemoveEmailTemplateCommandModel()
            {
                EmailTemplateId = EmailTemplate.Id,
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

            EmailTemplate = await database.EmailTemplates
                .Where(x => x.Id == CommandModel.EmailTemplateId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-email-templates");
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
