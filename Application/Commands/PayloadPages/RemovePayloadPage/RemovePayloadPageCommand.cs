namespace Lefish.Application.Commands.PayloadPages.RemovePayloadPage;

public class RemovePayloadPageCommand(IDatabaseService database) : IRemovePayloadPageCommand
{
    public async Task Execute(
        IUserToken userToken, RemovePayloadPageCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var page = await database.PayloadPages
            .Where(x => x.Id == model.PayloadPageId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.PayloadPages.Remove(page);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
