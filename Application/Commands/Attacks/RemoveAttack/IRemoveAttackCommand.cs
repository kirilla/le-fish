namespace Lefish.Application.Commands.Attacks.RemoveAttack;

public interface IRemoveAttackCommand
{
    Task Execute(IUserToken userToken, RemoveAttackCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
