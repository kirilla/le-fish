namespace Lefish.Application.Commands.Targets.AddTarget;

public interface IAddTargetCommand
{
    Task<int> Execute(IUserToken userToken, AddTargetCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
