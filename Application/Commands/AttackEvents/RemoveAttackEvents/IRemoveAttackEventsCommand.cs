namespace Lefish.Application.Commands.AttackEvents.RemoveAttackEvents;

public interface IRemoveAttackEventsCommand
{
    Task Execute(IUserToken userToken, RemoveAttackEventsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
