namespace Lefish.Application.Commands.EmailAttachments.EditEmailAttachment;

public class EditEmailAttachmentCommand(IDatabaseService database) : IEditEmailAttachmentCommand
{
    public async Task Execute(
        IUserToken userToken, EditEmailAttachmentCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var attachment = await database.EmailAttachments
            .Where(x => x.Id == model.EmailAttachmentId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.EmailAttachments
            .AnyAsync(x =>
                x.Name == model.Name &&
                x.Id != model.EmailAttachmentId))
            throw new BlockedByNameException();

        attachment.Name = model.Name;
        attachment.ContentType = model.ContentType;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
