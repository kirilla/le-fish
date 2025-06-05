namespace Lefish.Application.Commands.Targets.RemoveTarget;

public class RemoveTargetCommand(IDatabaseService database) : IRemoveTargetCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveTargetCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var target = await database.Targets
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.Targets.Remove(target);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
