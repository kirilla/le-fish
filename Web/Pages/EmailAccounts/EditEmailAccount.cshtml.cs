using Lefish.Application.Commands.EmailAccounts.EditEmailAccount;

namespace Lefish.Web.Pages.EmailAccounts;

public class EditEmailAccountModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditEmailAccountCommand command) : UserTokenPageModel(userToken)
{
    public EmailAccount EmailAccount { get; set; }

    [BindProperty]
    public EditEmailAccountCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailAccount = await database.EmailAccounts
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditEmailAccountCommandModel()
            {
                EmailAccountId = EmailAccount.Id,
                FromName = EmailAccount.FromName,
                FromAddress = EmailAccount.FromAddress,
                ReplyToName = EmailAccount.ReplyToName,
                ReplyToAddress = EmailAccount.ReplyToAddress,
                Password = EmailAccount.Password,
                SmtpHost = EmailAccount.SmtpHost,
                SmtpPort = EmailAccount.SmtpPort,
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

            EmailAccount = await database.EmailAccounts
                .Where(x => x.Id == CommandModel.EmailAccountId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-email-account/{id}");
        }
        catch (BlockedByNameException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.FromAddress),
                "Det finns ett annat epostkonto med samma adress.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
