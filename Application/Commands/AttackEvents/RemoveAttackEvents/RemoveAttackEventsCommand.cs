namespace Lefish.Application.Commands.AttackEvents.RemoveAttackEvents;

public class RemoveAttackEventsCommand(IDatabaseService database) : IRemoveAttackEventsCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveAttackEventsCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        if (model.EmailTargetId.HasValue)
        {
            await database.AttackEvents
                .Where(x => x.Attack.TargetId == model.EmailTargetId!.Value)
                .ExecuteDeleteAsync();
        }
        else
        {
            await database.AttackEvents.ExecuteDeleteAsync();
        }
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
