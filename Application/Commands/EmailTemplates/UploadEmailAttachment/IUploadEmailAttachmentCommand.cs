namespace Lefish.Application.Commands.EmailTemplates.UploadEmailAttachment;

public interface IUploadEmailAttachmentCommand
{
    Task Execute(IUserToken userToken, UploadEmailAttachmentCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
