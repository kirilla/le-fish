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

        if (await database.PayloadPages
            .AnyAsync(x => x.PageKey == model.PageKey))
            throw new BlockedByKeyException();

        var page = new PayloadPage()
        {
            PageKey = model.PageKey,
            Comment = model.Comment,
            Html = model.Html,
        };

        database.PayloadPages.Add(page);

        await database.SaveAsync(userToken);

        return page.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
