namespace Lefish.Application.Commands.EmailTemplates.RemoveEmailTemplate;

public class RemoveEmailTemplateCommand(IDatabaseService database) : IRemoveEmailTemplateCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveEmailTemplateCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var Template = await database.EmailTemplates
            .Where(x => x.Id == model.EmailTemplateId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.EmailTemplates.Remove(Template);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
