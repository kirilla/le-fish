namespace Lefish.Application.Commands.PayloadPages.RemovePayloadPage;

public interface IRemovePayloadPageCommand
{
    Task Execute(IUserToken userToken, RemovePayloadPageCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
