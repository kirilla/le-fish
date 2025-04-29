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

        var page = new PayloadPage()
        {
            Name = model.Name,
            Html = model.Html,
            PageKey = null,
        };

        var keys = await database.PayloadPages
            .Select(x => x.PageKey)
            .ToListAsync();

        while (page.PageKey == null)
        {
            page.PageKey = Random.Shared.Next();

            if (keys.Any(x => x == page.PageKey))
                page.PageKey = null;
        }

        database.PayloadPages.Add(page);

        await database.SaveAsync(userToken);

        return page.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
