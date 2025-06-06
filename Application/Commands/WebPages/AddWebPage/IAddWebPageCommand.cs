namespace Lefish.Application.Commands.WebPages.AddWebPage;

public interface IAddWebPageCommand
{
    Task<int> Execute(IUserToken userToken, AddWebPageCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
