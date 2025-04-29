namespace Lefish.Application.Commands.DataDumps.RemoveDataDump;

public interface IRemoveDataDumpCommand
{
    Task Execute(IUserToken userToken, RemoveDataDumpCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
