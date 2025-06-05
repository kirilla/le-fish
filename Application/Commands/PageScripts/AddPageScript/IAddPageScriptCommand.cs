namespace Lefish.Application.Commands.PageScripts.AddPageScript;

public interface IAddPageScriptCommand
{
    Task<int> Execute(IUserToken userToken, AddPageScriptCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
