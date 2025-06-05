namespace Lefish.Application.Commands.Targets.EditTarget;

public class EditTargetCommand(IDatabaseService database) : IEditTargetCommand
{
    public async Task Execute(
        IUserToken userToken, EditTargetCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var target = await database.Targets
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.Targets
            .AnyAsync(x =>
                x.Address == model.Address &&
                x.Id != model.Id))
            throw new BlockedByAddressException();

        target.Name = model.Name;
        target.Address = model.Address;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
