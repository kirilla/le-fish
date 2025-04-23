namespace Lefish.Application.Commands.EmailTargets.EditEmailTarget;

public interface IEditEmailTargetCommand
{
    Task Execute(IUserToken userToken, EditEmailTargetCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
