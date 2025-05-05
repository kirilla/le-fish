namespace Lefish.Application.Commands.BlockedRequests.RemoveBlockedRequests;

public class RemoveBlockedRequestsCommand(IDatabaseService database) : IRemoveBlockedRequestsCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveBlockedRequestsCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        await database.BlockedRequests.ExecuteDeleteAsync();
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
