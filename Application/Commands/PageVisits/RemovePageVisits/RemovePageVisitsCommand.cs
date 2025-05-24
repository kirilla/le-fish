namespace Lefish.Application.Commands.PageVisits.RemovePageVisits;

public class RemovePageVisitsCommand(IDatabaseService database) : IRemovePageVisitsCommand
{
    public async Task Execute(
        IUserToken userToken, RemovePageVisitsCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        if (model.EmailTargetId.HasValue)
        {
            await database.PageVisits
                .Where(x => x.PageKey.EmailMessage.EmailTargetId == model.EmailTargetId!.Value)
                .ExecuteDeleteAsync();
        }
        else
        {
            await database.PageVisits.ExecuteDeleteAsync();
        }
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
