namespace Lefish.Application.Commands.EmailImages.RemoveEmailImage;

public interface IRemoveEmailImageCommand
{
    Task Execute(IUserToken userToken, RemoveEmailImageCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
