namespace Lefish.Application.Commands.PayloadPages.EditWebPage;

public interface IEditWebPageCommand
{
    Task Execute(IUserToken userToken, EditWebPageCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
