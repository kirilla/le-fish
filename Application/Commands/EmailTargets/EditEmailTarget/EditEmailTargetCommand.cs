namespace Lefish.Application.Commands.EmailTargets.EditEmailTarget;

public class EditEmailTargetCommand(IDatabaseService database) : IEditEmailTargetCommand
{
    public async Task Execute(
        IUserToken userToken, EditEmailTargetCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var target = await database.EmailTargets
            .Where(x => x.Id == model.EmailTargetId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.EmailTargets
            .AnyAsync(x =>
                x.Address == model.Address &&
                x.Id != model.EmailTargetId))
            throw new BlockedByAddressException();

        if (await database.EmailTargets
            .AnyAsync(x =>
                x.PersonKey == model.PersonKey &&
                x.Id != model.EmailTargetId))
            throw new BlockedByKeyException();

        target.Name = model.Name;
        target.Address = model.Address;
        target.PersonKey = model.PersonKey;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
