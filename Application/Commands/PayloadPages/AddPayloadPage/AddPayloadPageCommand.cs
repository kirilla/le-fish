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
            UrlRegex = model.UrlRegex,
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
