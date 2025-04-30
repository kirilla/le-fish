namespace Lefish.Application.Commands.PayloadPages.EditPayloadPage;

public class EditPayloadPageCommand(IDatabaseService database) : IEditPayloadPageCommand
{
    public async Task Execute(
        IUserToken userToken, EditPayloadPageCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();
        model.TruncateByStringLength();

        var page = await database.PayloadPages
            .Where(x => x.Id == model.PayloadPageId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        page.Name = model.Name;
        page.PageKey = model.PageKey;
        page.Html = model.Html;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
