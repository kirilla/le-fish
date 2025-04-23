using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.EmailMessages.SendEmailToTarget;

public class SendEmailToTargetCommandModel
{
    [Required]
    public int? EmailTargetId { get; set; }

    [Required]
    public int? EmailTemplateId { get; set; }

    [Required]
    public int? EmailAccountId { get; set; }
}
