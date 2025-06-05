namespace Lefish.Application.Commands.EmailTargets.RemoveEmailTarget;

public class RemoveEmailTargetCommand(IDatabaseService database) : IRemoveEmailTargetCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveEmailTargetCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var target = await database.Targets
            .Where(x => x.Id == model.EmailTargetId)
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
