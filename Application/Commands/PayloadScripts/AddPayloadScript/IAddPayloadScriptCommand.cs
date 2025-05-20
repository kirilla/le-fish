namespace Lefish.Application.Commands.PayloadScripts.AddPayloadScript;

public interface IAddPayloadScriptCommand
{
    Task<int> Execute(IUserToken userToken, AddPayloadScriptCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
