namespace Lefish.Application.Commands.Visits.RemoveVisit;

public class RemoveVisitCommand(IDatabaseService database) : IRemoveVisitCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveVisitCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var visit = await database.Visits
            .Where(x => x.Id == model.VisitId)
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
