namespace Lefish.Application.Commands.PageVisits.RemovePageVisit;

public class RemovePageVisitCommand(IDatabaseService database) : IRemovePageVisitCommand
{
    public async Task Execute(
        IUserToken userToken, RemovePageVisitCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var visit = await database.Visits
            .Where(x => x.Id == model.PageVisitId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.Visits.Remove(visit);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
