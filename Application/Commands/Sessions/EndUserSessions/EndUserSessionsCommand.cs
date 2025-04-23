namespace Lefish.Application.Commands.Sessions.EndUserSessions;

public class EndUserSessionsCommand(
    IDateService dateService,
    IDatabaseService database) : IEndUserSessionsCommand
{
    public async Task Execute(
        IUserToken userToken, EndUserSessionsCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        var sessions = await database.Sessions.ToListAsync();

        database.Sessions.RemoveRange(sessions);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
