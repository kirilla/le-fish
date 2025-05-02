namespace Lefish.Application.Commands.IpRanges.RemoveIpRange;

public interface IRemoveIpRangeCommand
{
    Task Execute(IUserToken userToken, RemoveIpRangeCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
