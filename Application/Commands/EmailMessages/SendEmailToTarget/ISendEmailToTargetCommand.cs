namespace Lefish.Application.Commands.EmailMessages.SendEmailToTarget;

public interface ISendEmailToTargetCommand
{
    Task Execute(IUserToken userToken, SendEmailToTargetCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
