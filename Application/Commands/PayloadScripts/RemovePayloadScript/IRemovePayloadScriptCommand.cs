namespace Lefish.Application.Commands.PayloadScripts.RemovePayloadScript;

public interface IRemovePayloadScriptCommand
{
    Task Execute(IUserToken userToken, RemovePayloadScriptCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
