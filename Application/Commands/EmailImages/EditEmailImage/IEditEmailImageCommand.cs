namespace Lefish.Application.Commands.EmailImages.EditEmailImage;

public interface IEditEmailImageCommand
{
    Task Execute(IUserToken userToken, EditEmailImageCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
