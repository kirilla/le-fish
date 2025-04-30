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

        var page = new PayloadPage()
        {
            Name = model.Name,
            Html = model.Html,
            PageKey = null,
        };

        var keys = await database.PayloadPages
            .Select(x => x.PageKey)
            .ToListAsync();

        page.GeneratePhishingKey(keys);

        database.PayloadPages.Add(page);

        await database.SaveAsync(userToken);

        return page.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
