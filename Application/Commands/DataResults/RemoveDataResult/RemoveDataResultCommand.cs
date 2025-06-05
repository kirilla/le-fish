namespace Lefish.Application.Commands.DataResults.RemoveDataResult;

public class RemoveDataResultCommand(IDatabaseService database) : IRemoveDataResultCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveDataResultCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var result = await database.DataResults
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.DataResults.Remove(result);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
