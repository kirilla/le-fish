namespace Lefish.Application.Commands.Targets.AddTarget;

public class AddTargetCommand(IDatabaseService database) : IAddTargetCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddTargetCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.Targets
            .AnyAsync(x => x.Address == model.Address))
            throw new BlockedByAddressException();

        var target = new Target()
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
