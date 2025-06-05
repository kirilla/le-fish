namespace Lefish.Application.Commands.Targets.RemoveTarget;

public interface IRemoveTargetCommand
{
    Task Execute(IUserToken userToken, RemoveTargetCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
