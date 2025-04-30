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

        if (await database.EmailTargets
            .AnyAsync(x => x.Address == model.Address))
            throw new BlockedByAddressException();

        if (await database.EmailTargets
            .AnyAsync(x => x.PersonKey == model.PersonKey))
            throw new BlockedByKeyException();

        var target = new EmailTarget()
        {
            Name = model.Name,
            Address = model.Address,
            PersonKey = model.PersonKey,
        };

        database.EmailTargets.Add(target);

        await database.SaveAsync(userToken);

        return target.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
