namespace Lefish.Application.Commands.BlockedRequests.RemoveBlockedRequest;

public interface IRemoveBlockedRequestCommand
{
    Task Execute(IUserToken userToken, RemoveBlockedRequestCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
