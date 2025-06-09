using Lefish.Application.Commands.EmailMessages.SendEmailToTarget;

namespace Lefish.Web.Pages.EmailMessages;

public class SendEmailToTargetModel(
    IUserToken userToken,
    IDatabaseService database,
    ISendEmailToTargetCommand command) : UserTokenPageModel(userToken)
{
    public Target Target { get; set; }

    public List<EmailAccount> EmailAccounts { get; set; }
    public List<EmailTemplate> EmailTemplates { get; set; }

    public List<WebPageSummary> WebPages { get; set; }
    public List<PageScriptSummary> PageScripts { get; set; }

    public List<InstructionSet> InstructionSets { get; set; }

    [BindProperty]
    public SendEmailToTargetCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Target = await database.Targets
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

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

            CommandModel = new SendEmailToTargetCommandModel()
            {
                TargetId = id,
            };

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

    public async Task<IActionResult> OnPostAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Target = await database.Targets
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

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

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-target/{id}");
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
