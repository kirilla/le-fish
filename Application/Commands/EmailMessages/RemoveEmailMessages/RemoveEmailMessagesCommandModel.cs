using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.EmailMessages.RemoveEmailMessages;

public class RemoveEmailMessagesCommandModel
{
    [Required]
    public EmailStatus? EmailStatus { get; set; }
}
