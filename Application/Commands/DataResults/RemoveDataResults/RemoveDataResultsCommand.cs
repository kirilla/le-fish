namespace Lefish.Application.Commands.DataResults.RemoveDataResults;

public class RemoveDataResultsCommand(IDatabaseService database) : IRemoveDataResultsCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveDataResultsCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        await database.DataResults.ExecuteDeleteAsync();
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
