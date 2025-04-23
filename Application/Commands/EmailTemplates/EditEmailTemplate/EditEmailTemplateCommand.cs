namespace Lefish.Application.Commands.EmailTemplates.EditEmailTemplate;

public class EditEmailTemplateCommand(IDatabaseService database) : IEditEmailTemplateCommand
{
    public async Task Execute(
        IUserToken userToken, EditEmailTemplateCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var Template = await database.EmailTemplates
            .Where(x => x.Id == model.EmailTemplateId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        Template.Subject = model.Subject;
        Template.HtmlBody = model.HtmlBody;
        Template.TextBody = model.TextBody;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
