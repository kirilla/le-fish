namespace Lefish.Application.Commands.DataDumps.RemoveDataDumps;

public class RemoveDataDumpsCommand(IDatabaseService database) : IRemoveDataDumpsCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveDataDumpsCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        await database.DataDumps.ExecuteDeleteAsync();
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
