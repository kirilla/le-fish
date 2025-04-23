namespace Lefish.Application.Commands.PayloadPages.EditPayloadPage;

public interface IEditPayloadPageCommand
{
    Task Execute(IUserToken userToken, EditPayloadPageCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
