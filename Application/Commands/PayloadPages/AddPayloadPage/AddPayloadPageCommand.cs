namespace Lefish.Application.Commands.PayloadPages.AddPayloadPage;

public class AddPayloadPageCommand(IDatabaseService database) : IAddPayloadPageCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddPayloadPageCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();
        model.TruncateByStringLength();

        if (await database.WebPages
            .AnyAsync(x => x.Name == model.Name))
            throw new BlockedByExistingException();

        var page = new WebPage()
        {
            Name = model.Name,
            Html = model.Html,
        };

        database.WebPages.Add(page);

        await database.SaveAsync(userToken);

        return page.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
