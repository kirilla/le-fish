namespace Lefish.Application.Commands.DataResults.RemoveDataResult;

public interface IRemoveDataResultCommand
{
    Task Execute(IUserToken userToken, RemoveDataResultCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
