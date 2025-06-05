namespace Lefish.Application.Commands.PayloadScripts.RemovePayloadScript;

public class RemovePayloadScriptCommand(IDatabaseService database) : IRemovePayloadScriptCommand
{
    public async Task Execute(
        IUserToken userToken, RemovePayloadScriptCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var page = await database.PageScripts
            .Where(x => x.Id == model.PayloadScriptId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.PageScripts.Remove(page);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
