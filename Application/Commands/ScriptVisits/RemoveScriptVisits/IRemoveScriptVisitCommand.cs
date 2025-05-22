namespace Lefish.Application.Commands.ScriptVisits.RemoveScriptVisits;

public interface IRemoveScriptVisitsCommand
{
    Task Execute(IUserToken userToken, RemoveScriptVisitsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
