namespace Lefish.Application.Commands.EmailMessages.RemoveEmailMessage;

public class RemoveEmailMessageCommand(IDatabaseService database) : IRemoveEmailMessageCommand
{
    private readonly IDatabaseService _database = database;

    public async Task Execute(
        IUserToken userToken, RemoveEmailMessageCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var message = await _database.EmailMessages
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        _database.EmailMessages.Remove(message);

        await _database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
