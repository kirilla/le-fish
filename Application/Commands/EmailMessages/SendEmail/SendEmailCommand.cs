using Lefish.Common.Settings;
using Microsoft.Extensions.Options;

namespace Lefish.Application.Commands.EmailMessages.SendEmail;

public class SendEmailCommand(
    IDateService dateService,
    ISmtpService smtpService,
    IDatabaseService database,
    IOptions<TemplateConfiguration> templateConfiguration) : ISendEmailCommand
{
    private readonly TemplateConfiguration _config = templateConfiguration.Value;

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

        var page = await database.WebPages
            .Where(x => x.Id == model.PayloadPageId!.Value)
            .SingleOrDefaultAsync() ??
             throw new NotFoundException();

        var script = await database.PageScripts
            .Where(x => x.Id == model.PageScriptId!.Value)
            .SingleOrDefaultAsync() ??
             throw new NotFoundException();

        var target = await database.Targets
            .Where(x => x.Id == model.TargetId!.Value)
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

        var attack = new Attack()
        {
            TargetId = target.Id,
            WebPageId = page.Id,
            PageScriptId = script.Id,
            Value = Random.Shared.Next(),
        };

        database.Attacks.Add(attack);

        var message = new EmailMessage()
        {
            ToName = target.Name,
            ToAddress = target.Address,
            Subject = template.Subject,
            HtmlBody = template.HtmlBody,
            TextBody = template.TextBody,
            EmailStatus = EmailStatus.NotSent,
            EmailAccountId = account.Id,
            Attack = attack,
        };

        database.EmailMessages.Add(message);
        
        message.ReplaceVariables(_config, target, attack);

        await database.SaveAsync(userToken);

        try
        {
            smtpService.SendMessage(message, account, attachments, images);

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
