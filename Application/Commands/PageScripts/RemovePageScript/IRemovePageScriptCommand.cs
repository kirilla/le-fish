namespace Lefish.Application.Commands.PageScripts.RemovePageScript;

public interface IRemovePageScriptCommand
{
    Task Execute(IUserToken userToken, RemovePageScriptCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
