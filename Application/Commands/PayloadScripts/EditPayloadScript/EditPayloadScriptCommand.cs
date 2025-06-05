namespace Lefish.Application.Commands.PayloadScripts.EditPayloadScript;

public class EditPayloadScriptCommand(IDatabaseService database) : IEditPayloadScriptCommand
{
    public async Task Execute(
        IUserToken userToken, EditPayloadScriptCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();
        model.TruncateByStringLength();

        var page = await database.PageScripts
            .Where(x => x.Id == model.PayloadScriptId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.PageScripts
            .AnyAsync(x =>
                x.Name == model.Name &&
                x.Id != model.PayloadScriptId))
            throw new BlockedByExistingException();

        page.Name = model.Name;
        page.Script = model.Script;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
