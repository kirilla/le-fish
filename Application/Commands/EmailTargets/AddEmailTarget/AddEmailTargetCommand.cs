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
            throw new BlockedByExistingException();

        var target = new EmailTarget()
        {
            Name = model.Name,
            Address = model.Address,
            PersonKey = null,
        };

        var keys = await database.EmailTargets
            .Select(x => x.PersonKey)
            .ToListAsync();

        target.GeneratePhishingKey(keys);

        database.EmailTargets.Add(target);

        await database.SaveAsync(userToken);

        return target.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
