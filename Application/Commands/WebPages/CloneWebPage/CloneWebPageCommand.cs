namespace Lefish.Application.Commands.PayloadPages.CloneWebPage;

public class CloneWebPageCommand(IDatabaseService database) : ICloneWebPageCommand
{
    public async Task<int> Execute(
        IUserToken userToken, CloneWebPageCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var page = await database.WebPages
            .AsNoTracking()
            .Where(x => x.Id == model.PayloadPageId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.WebPages
            .AnyAsync(x => x.Name == model.Name))
            throw new BlockedByExistingException();

        var newPage = new WebPage()
        {
            Name = model.Name,
            Html = page.Html,
        };

        database.WebPages.Add(newPage);

        await database.SaveAsync(userToken);

        return newPage.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
