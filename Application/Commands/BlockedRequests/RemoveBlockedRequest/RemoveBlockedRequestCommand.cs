namespace Lefish.Application.Commands.BlockedRequests.RemoveBlockedRequest;

public class RemoveBlockedRequestCommand(IDatabaseService database) : IRemoveBlockedRequestCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveBlockedRequestCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var evt = await database.BlockedRequests
            .Where(x => x.Id == model.BlockedRequestId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.BlockedRequests.Remove(evt);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
