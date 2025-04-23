namespace Lefish.Application.Commands.EmailAttachments.RemoveEmailAttachment;

public interface IRemoveEmailAttachmentCommand
{
    Task Execute(IUserToken userToken, RemoveEmailAttachmentCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
