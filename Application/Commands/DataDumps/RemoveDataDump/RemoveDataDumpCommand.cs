namespace Lefish.Application.Commands.DataDumps.RemoveDataDump;

public class RemoveDataDumpCommand(IDatabaseService database) : IRemoveDataDumpCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveDataDumpCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var dump = await database.DataDumps
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.DataDumps.Remove(dump);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
