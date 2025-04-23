namespace Lefish.Application.Commands.EmailAttachments.EditEmailAttachment;

public interface IEditEmailAttachmentCommand
{
    Task Execute(IUserToken userToken, EditEmailAttachmentCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
