namespace Lefish.Application.Commands.DataResults.EditDataResult;

public interface IEditDataResultCommand
{
    Task Execute(IUserToken userToken, EditDataResultCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
