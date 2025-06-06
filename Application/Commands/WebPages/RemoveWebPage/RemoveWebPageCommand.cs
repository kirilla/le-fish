namespace Lefish.Application.Commands.WebPages.RemoveWebPage;

public class RemoveWebPageCommand(IDatabaseService database) : IRemoveWebPageCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveWebPageCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var page = await database.WebPages
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.WebPages.Remove(page);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
