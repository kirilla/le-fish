namespace Lefish.Application.Commands.EmailAccounts.AddEmailAccount;

public class AddEmailAccountCommand(IDatabaseService database) : IAddEmailAccountCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddEmailAccountCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.EmailAccounts
            .AnyAsync(x => x.FromAddress == model.FromAddress))
            throw new BlockedByExistingException();

        var account = new EmailAccount()
        {
            FromName = model.FromName,
            FromAddress = model.FromAddress,
            ReplyToName = model.ReplyToName,
            ReplyToAddress = model.ReplyToAddress,
            Password = model.Password,
            SmtpHost = model.SmtpHost,
            SmtpPort = model.SmtpPort,
        };

        database.EmailAccounts.Add(account);

        await database.SaveAsync(userToken);

        return account.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
