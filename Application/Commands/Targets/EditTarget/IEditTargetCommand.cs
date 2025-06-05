namespace Lefish.Application.Commands.Targets.EditTarget;

public interface IEditTargetCommand
{
    Task Execute(IUserToken userToken, EditTargetCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
