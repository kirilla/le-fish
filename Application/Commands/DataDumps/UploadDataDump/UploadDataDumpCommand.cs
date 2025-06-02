namespace Lefish.Application.Commands.DataDumps.UploadDataDump;

public class UploadDataDumpCommand(IDatabaseService database) : IUploadDataDumpCommand
{
    public async Task Execute(
        IUserToken userToken,
        UploadDataDumpCommandModel model,
        int pageKeyValue)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();
        model.TruncateByStringLength();

        var pageKey = await database.Attacks
            .Where(x => x.Value == pageKeyValue)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var dump = new DataDump()
        {
            PageKeyId = pageKey.Id,
            JsonData = model.JsonData,
        };

        database.DataDumps.Add(dump);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        //return userToken.IsAuthenticated;

        return true;

        // TODO: Add an app settings property?
    }
}
