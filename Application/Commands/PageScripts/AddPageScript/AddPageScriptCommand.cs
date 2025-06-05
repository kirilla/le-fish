namespace Lefish.Application.Commands.PageScripts.AddPageScript;

public class AddPageScriptCommand(IDatabaseService database) : IAddPageScriptCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddPageScriptCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();
        model.TruncateByStringLength();

        if (await database.PageScripts
            .AnyAsync(x => x.Name == model.Name))
            throw new BlockedByExistingException();

        var page = new PageScript()
        {
            Name = model.Name,
            Script = model.Script,
        };

        database.PageScripts.Add(page);

        await database.SaveAsync(userToken);

        return page.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
