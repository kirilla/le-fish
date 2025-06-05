namespace Lefish.Application.Commands.EmailTargets.AddEmailTarget;

public class AddEmailTargetCommand(IDatabaseService database) : IAddEmailTargetCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddEmailTargetCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.Targets
            .AnyAsync(x => x.Address == model.Address))
            throw new BlockedByAddressException();

        var target = new EmailTarget()
        {
            Name = model.Name,
            Address = model.Address,
        };

        database.Targets.Add(target);

        await database.SaveAsync(userToken);

        return target.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
