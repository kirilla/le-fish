using System.Net;

namespace Lefish.Application.Commands.IpRanges.AddIpRange;

public class AddIpRangeCommand(IDatabaseService database) : IAddIpRangeCommand
{
    public async Task Execute(
        IUserToken userToken, AddIpRangeCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (!model.Range.Contains("/"))
            throw new MissingPartException();

        if (model.Range.Split('/').Length != 2)
            throw new MissingPartException();

        IPNetwork.Parse(model.Range);

        if (await database.IpRanges
            .AnyAsync(x => x.Range == model.Range))
            throw new BlockedByExistingException();

        var range = new IpRange()
        {
            Range = model.Range,
            Blocked = model.Blocked
        };

        database.IpRanges.Add(range);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
