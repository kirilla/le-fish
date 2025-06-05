namespace Lefish.Application.Commands.PageScripts.EditPageScript;

public interface IEditPageScriptCommand
{
    Task Execute(IUserToken userToken, EditPageScriptCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
