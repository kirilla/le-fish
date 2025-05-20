namespace Lefish.Application.Commands.PayloadScripts.EditPayloadScript;

public interface IEditPayloadScriptCommand
{
    Task Execute(IUserToken userToken, EditPayloadScriptCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
