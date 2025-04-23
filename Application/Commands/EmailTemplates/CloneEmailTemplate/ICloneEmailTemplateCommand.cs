namespace Lefish.Application.Commands.EmailTemplates.CloneEmailTemplate;

public interface ICloneEmailTemplateCommand
{
    Task<int> Execute(IUserToken userToken, CloneEmailTemplateCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
