namespace Lefish.Application.Commands.EmailTemplates.UploadEmailImage;

public class UploadEmailImageCommand(IDatabaseService database) : IUploadEmailImageCommand
{
    public async Task Execute(
        IUserToken userToken, UploadEmailImageCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();
        model.TruncateByStringLength();

        var template = await database.EmailTemplates
            .Where(x => x.Id == model.EmailTemplateId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var image = new EmailImage()
        {
            EmailTemplateId = model.EmailTemplateId,
            Data = model.Data,
            ContentLength = model.Data.Length,
            ContentType = model.ContentType,
            Name = model.Name,
        };

        database.EmailImages.Add(image);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
