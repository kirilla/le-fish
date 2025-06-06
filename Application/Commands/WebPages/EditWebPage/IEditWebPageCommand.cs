namespace Lefish.Application.Commands.WebPages.EditWebPage;

public interface IEditWebPageCommand
{
    Task Execute(IUserToken userToken, EditWebPageCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
