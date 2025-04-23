namespace Lefish.Application.Commands.EmailTemplates.AddEmailTemplate;

public class AddEmailTemplateCommand(IDatabaseService database) : IAddEmailTemplateCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddEmailTemplateCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var Template = new EmailTemplate()
        {
            Subject = model.Subject,
            HtmlBody = model.HtmlBody,
            TextBody = model.TextBody,
        };

        database.EmailTemplates.Add(Template);

        await database.SaveAsync(userToken);

        return Template.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
