using System.ComponentModel.DataAnnotations;
using Lefish.Common.Validation;

namespace Lefish.Application.Commands.EmailAttachments.EditEmailAttachment;

public class EditEmailAttachmentCommandModel
{
    public int EmailAttachmentId { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange filnamn.")]
    [StringLength(MaxLengths.Domain.EmailAttachment.Name)]
    public string Name { get; set; }

    [RegularExpression(Pattern.Common.Mime.Type)]
    [Required(ErrorMessage = "Ange filtyp.")]
    [StringLength(MaxLengths.Domain.EmailAttachment.ContentType)]
    public string ContentType { get; set; }
}
