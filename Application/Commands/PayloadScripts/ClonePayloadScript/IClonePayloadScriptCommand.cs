namespace Lefish.Application.Commands.PayloadScripts.ClonePayloadScript;

public interface IClonePayloadScriptCommand
{
    Task<int> Execute(IUserToken userToken, ClonePayloadScriptCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
