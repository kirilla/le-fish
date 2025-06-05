namespace Lefish.Application.Commands.AttackEvents.RemoveAttackEvent;

public interface IRemoveAttackEventCommand
{
    Task Execute(IUserToken userToken, RemoveAttackEventCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
