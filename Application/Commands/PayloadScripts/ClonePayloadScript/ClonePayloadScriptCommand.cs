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

        var page = await database.PageScripts
            .AsNoTracking()
            .Where(x => x.Id == model.PayloadScriptId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.PageScripts
            .AnyAsync(x => x.Name == model.Name))
            throw new BlockedByExistingException();

        var newPage = new PageScript()
        {
            Name = model.Name,
            Script = page.Script,
        };

        database.PageScripts.Add(newPage);

        await database.SaveAsync(userToken);

        return newPage.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
