namespace Lefish.Application.Commands.PayloadPages.RemovePayloadPage;

public interface IRemoveWebPageCommand
{
    Task Execute(IUserToken userToken, RemoveWebPageCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
