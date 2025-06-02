namespace Lefish.Application.Commands.Attacks.RemoveAttack;

public class RemoveAttackCommand(IDatabaseService database) : IRemoveAttackCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveAttackCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var key = await database.Attacks
            .Where(x => x.Id == model.PageKeyId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.Attacks.Remove(key);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
