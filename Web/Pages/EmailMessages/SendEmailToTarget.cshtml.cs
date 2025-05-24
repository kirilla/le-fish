using Lefish.Application.Commands.EmailMessages.SendEmailToTarget;

namespace Lefish.Web.Pages.EmailMessages;

public class SendEmailToTargetModel(
    IUserToken userToken,
    IDatabaseService database,
    ISendEmailToTargetCommand command) : UserTokenPageModel(userToken)
{
    public EmailTarget EmailTarget { get; set; }

    public List<EmailAccount> EmailAccounts { get; set; }
    public List<EmailTemplate> EmailTemplates { get; set; }

    public List<SimplePayloadPage> PayloadPages { get; set; }
    public List<SimplePayloadScript> PayloadScripts { get; set; }

    [BindProperty]
    public SendEmailToTargetCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailTarget = await database.EmailTargets
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            EmailAccounts = await database.EmailAccounts.ToListAsync();
            EmailTemplates = await database.EmailTemplates.ToListAsync();

            PayloadPages = await database.PayloadPages
                .Select(x => new SimplePayloadPage()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            PayloadScripts = await database.PayloadScripts
                .Select(x => new SimplePayloadScript()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            CommandModel = new SendEmailToTargetCommandModel()
            {
                EmailTargetId = id,
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

            EmailTarget = await database.EmailTargets
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            EmailAccounts = await database.EmailAccounts.ToListAsync();
            EmailTemplates = await database.EmailTemplates.ToListAsync();

            PayloadPages = await database.PayloadPages
                .Select(x => new SimplePayloadPage()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            PayloadScripts = await database.PayloadScripts
                .Select(x => new SimplePayloadScript()
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
