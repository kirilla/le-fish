namespace Lefish.Application.Commands.DataDumps.UploadDataDump;

public class UploadDataDumpCommand(IDatabaseService database) : IUploadDataDumpCommand
{
    public async Task Execute(
        IUserToken userToken,
        UploadDataDumpCommandModel model,
        int attackToken)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();
        model.TruncateByStringLength();

        var attack = await database.Attacks
            .Where(x => x.Value == attackToken)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var dump = new DataResult()
        {
            AttackId = attack.Id,
            JsonData = model.JsonData,
        };

        database.DataResults.Add(dump);

        var evt = new AttackEvent()
        {
            AttackId = attack.Id,
            AttackEventKind = AttackEventKind.UploadData,
            Url = model.Url,
            Method = model.Method,
            IpAddress = model.IpAddress,
            UserAgent = model.UserAgent,
        };

        database.AttackEvents.Add(evt);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        //return userToken.IsAuthenticated;

        return true;

        // TODO: Add an app settings property?
    }
}
