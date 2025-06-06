namespace Lefish.Application.Commands.WebPages.RemoveWebPage;

public interface IRemoveWebPageCommand
{
    Task Execute(IUserToken userToken, RemoveWebPageCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
