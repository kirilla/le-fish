namespace Lefish.Application.Commands.EmailTemplates.EditEmailTemplate;

public interface IEditEmailTemplateCommand
{
    Task Execute(IUserToken userToken, EditEmailTemplateCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
