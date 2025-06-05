namespace Lefish.Application.Commands.AttackEvents.RemoveAttackEvent;

public class RemoveAttackEventCommand(IDatabaseService database) : IRemoveAttackEventCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveAttackEventCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var evt = await database.AttackEvents
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.AttackEvents.Remove(evt);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
