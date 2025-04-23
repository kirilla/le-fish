namespace Lefish.Application.Commands.EmailTemplates.AddEmailTemplate;

public interface IAddEmailTemplateCommand
{
    Task<int> Execute(IUserToken userToken, AddEmailTemplateCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
