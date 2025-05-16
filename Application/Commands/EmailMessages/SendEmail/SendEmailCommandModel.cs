using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.EmailMessages.SendEmail;

public class SendEmailCommandModel
{
    [Required]
    public int? EmailTargetId { get; set; }

    [Required]
    public int? EmailTemplateId { get; set; }

    [Required]
    public int? EmailAccountId { get; set; }

    [Required]
    public int? PayloadPageId { get; set; }
}
