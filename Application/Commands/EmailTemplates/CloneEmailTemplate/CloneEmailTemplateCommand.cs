namespace Lefish.Application.Commands.EmailTemplates.CloneEmailTemplate;

public class CloneEmailTemplateCommand(IDatabaseService database) : ICloneEmailTemplateCommand
{
    public async Task<int> Execute(
        IUserToken userToken, CloneEmailTemplateCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var template = await database.EmailTemplates
            .AsNoTracking()
            .Where(x => x.Id == model.EmailTemplateId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var attachments = await database.EmailAttachments
            .AsNoTracking()
            .Where(x => x.EmailTemplateId == model.EmailTemplateId)
            .ToListAsync();

        var images = await database.EmailImages
            .AsNoTracking()
            .Where(x => x.EmailTemplateId == model.EmailTemplateId)
            .ToListAsync();

        var newTemplate = new EmailTemplate()
        {
            Subject = model.CloneSubject,
            HtmlBody = template.HtmlBody,
            TextBody = template.TextBody,
        };

        database.EmailTemplates.Add(newTemplate);

        foreach (var attachment in attachments)
        {
            var newAttachment = new EmailAttachment()
            {
                Data = attachment.Data,
                ContentLength = attachment.ContentLength,
                Name = attachment.Name,
                ContentType = attachment.ContentType,
                EmailTemplate = newTemplate,
            };

            database.EmailAttachments.Add(newAttachment);
        }

        foreach (var image in images)
        {
            var newImage = new EmailImage()
            {
                Data = image.Data,
                ContentLength = image.ContentLength,
                ContentType = image.ContentType,
                Name = image.Name,
                EmailTemplate = newTemplate,
            };

            database.EmailImages.Add(newImage);
        }

        await database.SaveAsync(userToken);

        return newTemplate.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
