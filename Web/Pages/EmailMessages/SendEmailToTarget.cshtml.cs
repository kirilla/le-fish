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

    public List<WebPageSummary> PayloadPages { get; set; }
    public List<PageScriptSummary> PageScripts { get; set; }

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

            EmailAccounts = await database.EmailAccounts.ToListAsync();
            EmailTemplates = await database.EmailTemplates.ToListAsync();

            PayloadPages = await database.WebPages
                .Select(x => new WebPageSummary()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            PageScripts = await database.PageScripts
                .Select(x => new PageScriptSummary()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
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

            EmailAccounts = await database.EmailAccounts.ToListAsync();
            EmailTemplates = await database.EmailTemplates.ToListAsync();

            PayloadPages = await database.WebPages
                .Select(x => new WebPageSummary()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            PageScripts = await database.PageScripts
                .Select(x => new PageScriptSummary()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-email-target/{id}");
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
