namespace Lefish.Application.Commands.EmailMessages.SendEmail;

public class SendEmailCommand(
    IDateService dateService,
    ISmtpService smtpService,
    IDatabaseService database) : ISendEmailCommand
{
    public async Task Execute(
        IUserToken userToken, SendEmailCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var account = await database.EmailAccounts
            .Where(x => x.Id == model.EmailAccountId!.Value)
            .SingleOrDefaultAsync() ??
             throw new NotFoundException();

        var page = await database.PayloadPages
            .Where(x => x.Id == model.PayloadPageId!.Value)
            .SingleOrDefaultAsync() ??
             throw new NotFoundException();

        var target = await database.EmailTargets
            .Where(x => x.Id == model.EmailTargetId!.Value)
            .SingleOrDefaultAsync() ??
             throw new NotFoundException();

        var template = await database.EmailTemplates
            .Where(x => x.Id == model.EmailTemplateId!.Value)
            .SingleOrDefaultAsync() ??
             throw new NotFoundException();

        var attachments = await database.EmailAttachments
            .Where(x => x.EmailTemplateId == model.EmailTemplateId!.Value)
            .ToListAsync();

        var images = await database.EmailImages
            .Where(x => x.EmailTemplateId == model.EmailTemplateId!.Value)
            .ToListAsync();

        var message = new EmailMessage()
        {
            Subject = template.Subject,
            HtmlBody = template.HtmlBody,
            TextBody = template.TextBody,
            EmailStatus = EmailStatus.NotSent,
            EmailAccountId = account.Id,
            EmailTargetId = target.Id,
        };

        var token = new PageToken()
        {
            EmailMessage = message,
            PayloadPageId = page.Id,
        };

        database.EmailMessages.Add(message);
        database.PageTokens.Add(token);

        await token.SetUniqueTokenAsync(database);

        message.InsertTargetValues(target, token);

        await database.SaveAsync(userToken);

        try
        {
            smtpService.SendMessage(target, message, account, attachments, images);

            message.EmailStatus = EmailStatus.Sent;
            message.Sent = dateService.GetDateTimeNow();
        }
        catch (Exception e)
        {
            message.EmailStatus = EmailStatus.SendFailed;
        }
        finally
        {
            await database.SaveAsync(userToken);
        }
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
