namespace Lefish.Application.Commands.EmailAccounts.EditEmailAccount;

public class EditEmailAccountCommand(IDatabaseService database) : IEditEmailAccountCommand
{
    public async Task Execute(
        IUserToken userToken, EditEmailAccountCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var account = await database.EmailAccounts
            .Where(x => x.Id == model.EmailAccountId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.EmailAccounts
            .AnyAsync(x =>
                x.FromName == model.FromName &&
                x.Id != model.EmailAccountId))
            throw new BlockedByNameException();

        account.FromName = model.FromName;
        account.FromAddress = model.FromAddress;
        account.ReplyToName = model.ReplyToName;
        account.ReplyToAddress = model.ReplyToAddress;
        account.Password = model.Password;
        account.SmtpHost = model.SmtpHost;
        account.SmtpPort = model.SmtpPort;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
