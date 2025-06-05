namespace Lefish.Application.Commands.DataDumps.EditDataDump;

public class EditDataDumpCommand(IDatabaseService database) : IEditDataDumpCommand
{
    public async Task Execute(
        IUserToken userToken, EditDataDumpCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var dump = await database.DataResults
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        dump.JsonData = model.JsonData;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
