namespace Lefish.Application.Commands.IpRanges.AddIpRange;

public interface IAddIpRangeCommand
{
    Task Execute(IUserToken userToken, AddIpRangeCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
