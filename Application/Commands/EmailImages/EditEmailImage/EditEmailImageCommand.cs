namespace Lefish.Application.Commands.EmailImages.EditEmailImage;

public class EditEmailImageCommand(IDatabaseService database) : IEditEmailImageCommand
{
    public async Task Execute(
        IUserToken userToken, EditEmailImageCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var image = await database.EmailImages
            .Where(x => x.Id == model.EmailImageId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.EmailImages
            .AnyAsync(x =>
                x.Name == model.Name &&
                x.Id != model.EmailImageId))
            throw new BlockedByNameException();

        image.Name = model.Name;
        image.ContentType = model.ContentType;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
