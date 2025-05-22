namespace Lefish.Application.Commands.ScriptVisits.RemoveScriptVisit;

public interface IRemoveScriptVisitCommand
{
    Task Execute(IUserToken userToken, RemoveScriptVisitCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
