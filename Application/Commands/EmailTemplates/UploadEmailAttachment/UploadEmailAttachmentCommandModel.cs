using System.ComponentModel.DataAnnotations;
using Lefish.Common.Validation;

namespace Lefish.Application.Commands.EmailTemplates.UploadEmailAttachment;

public class UploadEmailAttachmentCommandModel
{
    public int EmailTemplateId { get; set; }

    public byte[] Data { get; set; }

    //public int ContentLength { get; set; }

    [Required]
    [StringLength(MaxLengths.Domain.EmailAttachment.Name)]
    public string Name { get; set; }

    [Required]
    [StringLength(MaxLengths.Domain.EmailAttachment.ContentType)]
    public string ContentType { get; set; }
}
