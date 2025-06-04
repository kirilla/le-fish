namespace Lefish.Application.Commands.Visits.RemoveVisits;

public class RemoveVisitsCommand(IDatabaseService database) : IRemoveVisitsCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveVisitsCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        if (model.EmailTargetId.HasValue)
        {
            await database.Visits
                .Where(x => x.Attack.EmailTargetId == model.EmailTargetId!.Value)
                .ExecuteDeleteAsync();
        }
        else
        {
            await database.Visits.ExecuteDeleteAsync();
        }
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
