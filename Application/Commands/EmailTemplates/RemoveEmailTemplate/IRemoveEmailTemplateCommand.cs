namespace Lefish.Application.Commands.EmailTemplates.RemoveEmailTemplate;

public interface IRemoveEmailTemplateCommand
{
    Task Execute(IUserToken userToken, RemoveEmailTemplateCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
