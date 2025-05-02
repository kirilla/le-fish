using System.Net;

namespace Lefish.Application.Commands.IpRanges.EditIpRange;

public class EditIpRangeCommand(IDatabaseService database) : IEditIpRangeCommand
{
    public async Task Execute(
        IUserToken userToken, EditIpRangeCommandModel model)
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

        var range = await database.IpRanges
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.IpRanges
            .AnyAsync(x => 
                x.Range == model.Range &&
                x.Id != model.Id))
            throw new BlockedByExistingException();

        range.Range = model.Range;
        range.Blocked = model.Blocked;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
