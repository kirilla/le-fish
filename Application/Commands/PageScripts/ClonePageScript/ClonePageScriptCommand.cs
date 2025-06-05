namespace Lefish.Application.Commands.PageScripts.ClonePageScript;

public class ClonePageScriptCommand(IDatabaseService database) : IClonePageScriptCommand
{
    public async Task<int> Execute(
        IUserToken userToken, ClonePageScriptCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var page = await database.PageScripts
            .AsNoTracking()
            .Where(x => x.Id == model.Id)
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
