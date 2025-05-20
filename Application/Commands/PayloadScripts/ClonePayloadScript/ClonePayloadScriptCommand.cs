namespace Lefish.Application.Commands.PayloadScripts.ClonePayloadScript;

public class ClonePayloadScriptCommand(IDatabaseService database) : IClonePayloadScriptCommand
{
    public async Task<int> Execute(
        IUserToken userToken, ClonePayloadScriptCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var page = await database.PayloadScripts
            .AsNoTracking()
            .Where(x => x.Id == model.PayloadScriptId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.PayloadScripts
            .AnyAsync(x => x.Name == model.Name))
            throw new BlockedByExistingException();

        var newPage = new PayloadScript()
        {
            Name = model.Name,
            Script = page.Script,
        };

        database.PayloadScripts.Add(newPage);

        await database.SaveAsync(userToken);

        return newPage.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
