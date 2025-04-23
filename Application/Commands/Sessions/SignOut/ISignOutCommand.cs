namespace Lefish.Application.Commands.Sessions.SignOut;

public interface ISignOutCommand
{
    Task Execute(IUserToken userToken);

    bool IsPermitted(IUserToken userToken);
}
