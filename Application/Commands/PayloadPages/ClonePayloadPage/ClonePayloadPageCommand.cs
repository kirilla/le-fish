namespace Lefish.Application.Commands.PayloadPages.ClonePayloadPage;

public class ClonePayloadPageCommand(IDatabaseService database) : IClonePayloadPageCommand
{
    public async Task<int> Execute(
        IUserToken userToken, ClonePayloadPageCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var page = await database.PayloadPages
            .AsNoTracking()
            .Where(x => x.Id == model.PayloadPageId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.PayloadPages
            .AnyAsync(x => x.PageKey == model.PageKey))
            throw new BlockedByKeyException();

        var newPage = new PayloadPage()
        {
            PageKey = model.PageKey,
            Comment = model.Comment,
            Html = page.Html,
        };

        database.PayloadPages.Add(newPage);

        await database.SaveAsync(userToken);

        return newPage.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
