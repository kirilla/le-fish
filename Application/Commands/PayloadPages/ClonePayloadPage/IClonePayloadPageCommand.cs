namespace Lefish.Application.Commands.PayloadPages.ClonePayloadPage;

public interface IClonePayloadPageCommand
{
    Task<int> Execute(IUserToken userToken, ClonePayloadPageCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
