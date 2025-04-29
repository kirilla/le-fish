namespace Lefish.Application.Commands.DataDumps.EditDataDump;

public interface IEditDataDumpCommand
{
    Task Execute(IUserToken userToken, EditDataDumpCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
