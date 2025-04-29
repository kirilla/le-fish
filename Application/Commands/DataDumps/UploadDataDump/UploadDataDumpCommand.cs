namespace Lefish.Application.Commands.DataDumps.UploadDataDump;

public class UploadDataDumpCommand(IDatabaseService database) : IUploadDataDumpCommand
{
    public async Task Execute(
        IUserToken userToken, UploadDataDumpCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();
        model.TruncateByStringLength();

        var target = await database.EmailTargets
            .Where(x => x.Id == model.EmailTargetId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var dump = new DataDump()
        {
            EmailTargetId = model.EmailTargetId,
            Data = model.Data,
            ContentLength = model.Data.Length,
            ContentType = model.ContentType,
            Name = model.Name,
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
