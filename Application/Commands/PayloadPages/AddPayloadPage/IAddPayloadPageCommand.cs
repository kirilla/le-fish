namespace Lefish.Application.Commands.PayloadPages.AddPayloadPage;

public interface IAddPayloadPageCommand
{
    Task<int> Execute(IUserToken userToken, AddPayloadPageCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
