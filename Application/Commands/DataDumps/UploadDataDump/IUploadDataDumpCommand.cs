namespace Lefish.Application.Commands.DataDumps.UploadDataDump;

public interface IUploadDataDumpCommand
{
    Task Execute(
        IUserToken userToken, 
        UploadDataDumpCommandModel model, 
        int attackToken);

    bool IsPermitted(IUserToken userToken);
}
