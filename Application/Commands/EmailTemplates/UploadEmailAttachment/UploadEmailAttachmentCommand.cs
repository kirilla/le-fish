namespace Lefish.Application.Commands.EmailTemplates.UploadEmailAttachment;

public class UploadEmailAttachmentCommand(IDatabaseService database) : IUploadEmailAttachmentCommand
{
    public async Task Execute(
        IUserToken userToken, UploadEmailAttachmentCommandModel model)
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

        var attachment = new EmailAttachment()
        {
            EmailTemplateId = model.EmailTemplateId,
            Data = model.Data,
            ContentLength = model.Data.Length,
            ContentType = model.ContentType,
            Name = model.Name,
        };

        database.EmailAttachments.Add(attachment);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
