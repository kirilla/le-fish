namespace Lefish.Application.Commands.PayloadScripts.AddPayloadScript;

public class AddPayloadScriptCommand(IDatabaseService database) : IAddPayloadScriptCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddPayloadScriptCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();
        model.TruncateByStringLength();

        if (await database.PayloadScripts
            .AnyAsync(x => x.Name == model.Name))
            throw new BlockedByExistingException();

        var page = new PayloadScript()
        {
            Name = model.Name,
            Script = model.Script,
        };

        database.PayloadScripts.Add(page);

        await database.SaveAsync(userToken);

        return page.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
