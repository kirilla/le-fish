namespace Lefish.Application.Commands.EmailTargets.AddEmailTarget;

public interface IAddEmailTargetCommand
{
    Task<int> Execute(IUserToken userToken, AddEmailTargetCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
