namespace Lefish.Application.Commands.PageKeys.RemovePageKey;

public class RemovePageKeyCommand(IDatabaseService database) : IRemovePageKeyCommand
{
    public async Task Execute(
        IUserToken userToken, RemovePageKeyCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var key = await database.PageKeys
            .Where(x => x.Id == model.PageKeyId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.PageKeys.Remove(key);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
