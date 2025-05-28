namespace Lefish.Application.Commands.DataDumps.RemoveDataDumps;

public interface IRemoveDataDumpsCommand
{
    Task Execute(IUserToken userToken, RemoveDataDumpsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
