namespace Lefish.Application.Commands.EmailTargets.RemoveEmailTarget;

public interface IRemoveEmailTargetCommand
{
    Task Execute(IUserToken userToken, RemoveEmailTargetCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
