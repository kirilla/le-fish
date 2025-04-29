namespace Lefish.Application.Commands.DataDumps.UploadDataDump;

public interface IUploadDataDumpCommand
{
    Task Execute(IUserToken userToken, UploadDataDumpCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
