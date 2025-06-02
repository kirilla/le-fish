namespace Lefish.Application.Commands.ScriptVisits.RemoveScriptVisits;

public class RemoveScriptVisitsCommand(IDatabaseService database) : IRemoveScriptVisitsCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveScriptVisitsCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        if (model.EmailTargetId.HasValue)
        {
            await database.ScriptVisits
                .Where(x => x.Attack.EmailTargetId == model.EmailTargetId!.Value)
                .ExecuteDeleteAsync();
        }
        else
        {
            await database.ScriptVisits.ExecuteDeleteAsync();
        }
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
