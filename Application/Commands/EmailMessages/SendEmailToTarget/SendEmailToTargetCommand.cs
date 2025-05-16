namespace Lefish.Application.Commands.EmailMessages.SendEmailToTarget;

public class SendEmailToTargetCommand(
    IDateService dateService,
    ISmtpService smtpService,
    IDatabaseService database) : ISendEmailToTargetCommand
{
    public async Task Execute(
        IUserToken userToken, SendEmailToTargetCommandModel model)
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

        message.InsertTargetValues(target);

        database.EmailMessages.Add(message);

        var token = new PhishingToken()
        {
            EmailTargetId = target.Id,
            PayloadPageId = page.Id,
        };

        await token.SetUniqueTokenAsync(database);

        database.PhishingTokens.Add(token);

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
