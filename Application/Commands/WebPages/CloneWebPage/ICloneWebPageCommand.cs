namespace Lefish.Application.Commands.WebPages.CloneWebPage;

public interface ICloneWebPageCommand
{
    Task<int> Execute(IUserToken userToken, CloneWebPageCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
