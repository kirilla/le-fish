namespace Lefish.Application.Commands.BlockedRequests.RemoveBlockedRequests;

public interface IRemoveBlockedRequestsCommand
{
    Task Execute(IUserToken userToken, RemoveBlockedRequestsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
