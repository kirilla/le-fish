using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.EmailMessages.SendEmail;

public class SendEmailCommandModel
{
    [Required]
    public int? TargetId { get; set; }

    [Required]
    public int? EmailTemplateId { get; set; }

    [Required]
    public int? EmailAccountId { get; set; }

    [Required]
    public int? WebPageId { get; set; }

    [Required]
    public int? PageScriptId { get; set; }
}
