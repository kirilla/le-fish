namespace Lefish.Application.Commands.ScriptVisits.RemoveScriptVisit;

public class RemoveScriptVisitCommand(IDatabaseService database) : IRemoveScriptVisitCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveScriptVisitCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var visit = await database.ScriptVisits
            .Where(x => x.Id == model.ScriptVisitId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.ScriptVisits.Remove(visit);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
