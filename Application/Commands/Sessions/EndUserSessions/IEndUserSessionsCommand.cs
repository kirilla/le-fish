namespace Lefish.Application.Commands.Sessions.EndUserSessions;

public interface IEndUserSessionsCommand
{
    Task Execute(IUserToken userToken, EndUserSessionsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
