namespace Lefish.Application.Commands.IpRanges.EditIpRange;

public interface IEditIpRangeCommand
{
    Task Execute(IUserToken userToken, EditIpRangeCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
