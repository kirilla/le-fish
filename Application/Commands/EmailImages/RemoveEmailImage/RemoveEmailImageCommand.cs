namespace Lefish.Application.Commands.EmailImages.RemoveEmailImage;

public class RemoveEmailImageCommand(IDatabaseService database) : IRemoveEmailImageCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveEmailImageCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var image = await database.EmailImages
            .Where(x => x.Id == model.EmailImageId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.EmailImages.Remove(image);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
