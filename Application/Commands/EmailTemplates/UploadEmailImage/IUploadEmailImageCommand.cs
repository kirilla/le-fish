namespace Lefish.Application.Commands.EmailTemplates.UploadEmailImage;

public interface IUploadEmailImageCommand
{
    Task Execute(IUserToken userToken, UploadEmailImageCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
