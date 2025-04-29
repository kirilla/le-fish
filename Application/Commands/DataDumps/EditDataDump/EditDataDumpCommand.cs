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

        var dump = await database.DataDumps
            .Where(x => x.Id == model.DataDumpId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.DataDumps
            .AnyAsync(x =>
                x.Name == model.Name &&
                x.Id != model.DataDumpId))
            throw new BlockedByNameException();

        dump.Name = model.Name;
        dump.ContentType = model.ContentType;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
