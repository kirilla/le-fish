using Lefish.Application.Commands.EmailMessages.SendEmail;

namespace Lefish.Web.Pages.EmailMessages;

public class SendEmailModel(
    IUserToken userToken,
    IDatabaseService database,
    ISendEmailCommand command) : UserTokenPageModel(userToken)
{
    public List<EmailAccount> EmailAccounts { get; set; }
    public List<EmailTarget> EmailTargets { get; set; }
    public List<EmailTemplate> EmailTemplates { get; set; }

    public List<SimplePayloadPage> PayloadPages { get; set; }
    
    [BindProperty]
    public SendEmailCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailAccounts = await database.EmailAccounts.ToListAsync();
            EmailTargets = await database.EmailTargets.ToListAsync();
            EmailTemplates = await database.EmailTemplates.ToListAsync();

            PayloadPages = await database.PayloadPages
                .Select(x => new SimplePayloadPage()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            CommandModel = new SendEmailCommandModel();

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

            EmailAccounts = await database.EmailAccounts.ToListAsync();
            EmailTargets = await database.EmailTargets.ToListAsync();
            EmailTemplates = await database.EmailTemplates.ToListAsync();

            PayloadPages = await database.PayloadPages
                .Select(x => new SimplePayloadPage()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/desktop");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
