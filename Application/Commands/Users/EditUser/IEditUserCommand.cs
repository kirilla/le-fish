namespace Lefish.Application.Commands.Users.EditUser;

public interface IEditUserCommand
{
    Task Execute(IUserToken userToken, EditUserCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
