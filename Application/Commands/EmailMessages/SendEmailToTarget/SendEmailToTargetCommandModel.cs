using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.EmailMessages.SendEmailToTarget;

public class SendEmailToTargetCommandModel
{
    [Required]
    public int? TargetId { get; set; }

    [Required]
    public int? EmailTemplateId { get; set; }

    [Required]
    public int? EmailAccountId { get; set; }

    [Required]
    public int? PayloadPageId { get; set; }

    [Required]
    public int? PayloadScriptId { get; set; }
}
