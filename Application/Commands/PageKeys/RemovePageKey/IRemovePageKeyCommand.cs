namespace Lefish.Application.Commands.PageKeys.RemovePageKey;

public interface IRemovePageKeyCommand
{
    Task Execute(IUserToken userToken, RemovePageKeyCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
