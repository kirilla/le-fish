namespace Lefish.Application.Commands.EmailAttachments.RemoveEmailAttachment;

public class RemoveEmailAttachmentCommand(IDatabaseService database) : IRemoveEmailAttachmentCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveEmailAttachmentCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var attachment = await database.EmailAttachments
            .Where(x => x.Id == model.EmailAttachmentId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.EmailAttachments.Remove(attachment);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
