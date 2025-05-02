namespace Lefish.Application.Commands.IpRanges.RemoveIpRange;

public class RemoveIpRangeCommand(IDatabaseService database) : IRemoveIpRangeCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveIpRangeCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var range = await database.IpRanges
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.IpRanges.Remove(range);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
