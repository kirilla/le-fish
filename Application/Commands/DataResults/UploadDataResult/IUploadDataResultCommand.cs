namespace Lefish.Application.Commands.DataResults.UploadDataResult;

public interface IUploadDataResultCommand
{
    Task Execute(
        IUserToken userToken, 
        UploadDataResultCommandModel model, 
        int attackToken);

    bool IsPermitted(IUserToken userToken);
}
