namespace Lefish.Application.Commands.PageScripts.ClonePageScript;

public interface IClonePageScriptCommand
{
    Task<int> Execute(IUserToken userToken, ClonePageScriptCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
