using Lefish.Application.Commands.EmailMessages.SendEmail;

namespace Lefish.Web.Pages.EmailMessages;

public class SendEmailModel(
    IUserToken userToken,
    IDatabaseService database,
    ISendEmailCommand command) : UserTokenPageModel(userToken)
{
    public List<EmailAccount> EmailAccounts { get; set; }
    public List<Target> Targets { get; set; }
    public List<EmailTemplate> EmailTemplates { get; set; }

    public List<WebPageSummary> WebPages { get; set; }
    public List<PageScriptSummary> PageScripts { get; set; }

    public List<InstructionSet> InstructionSets { get; set; }

    [BindProperty]
    public SendEmailCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Targets = await database.Targets
                .OrderBy(x => x.Name)
                .ToListAsync();

            EmailAccounts = await database.EmailAccounts
                .OrderBy(x => x.FromAddress)
                .ToListAsync();
            
            EmailTemplates = await database.EmailTemplates
                .OrderBy(x => x.Subject)
                .ToListAsync();

            WebPages = await database.WebPages
                .OrderBy(x => x.Name)
                .Select(x => new WebPageSummary()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            PageScripts = await database.PageScripts
                .OrderBy(x => x.Name)
                .Select(x => new PageScriptSummary()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            InstructionSets = await database.InstructionSets
                .OrderBy(x => x.Name)
                .ToListAsync();

            CommandModel = new SendEmailCommandModel();

            if (EmailAccounts.Count == 1)
            {
                CommandModel.EmailAccountId = EmailAccounts[0].Id;
            }

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

            EmailAccounts = await database.EmailAccounts
                .OrderBy(x => x.FromAddress)
                .ToListAsync();
            
            Targets = await database.Targets
                .OrderBy(x => x.Name)
                .ToListAsync();
            
            EmailTemplates = await database.EmailTemplates
                .OrderBy(x => x.Subject)
                .ToListAsync();

            WebPages = await database.WebPages
                .OrderBy(x => x.Name)
                .Select(x => new WebPageSummary()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            PageScripts = await database.PageScripts
                .OrderBy(x => x.Name)
                .Select(x => new PageScriptSummary()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            InstructionSets = await database.InstructionSets
                .OrderBy(x => x.Name)
                .ToListAsync();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/show-attacks");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
