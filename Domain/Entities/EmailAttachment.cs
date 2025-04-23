using System.ComponentModel.DataAnnotations;

namespace Lefish.Domain.Entities;

public class EmailAttachment
{
    public int Id { get; set; }

    public byte[] Data { get; set; }

    public int ContentLength { get; set; }

    [Required]
    [StringLength(MaxLengths.Domain.EmailAttachment.Name)]
    public string Name { get; set; }

    [Required]
    [StringLength(MaxLengths.Domain.EmailAttachment.ContentType)]
    public string ContentType { get; set; }

    public int EmailTemplateId { get; set; }
    public EmailTemplate EmailTemplate { get; set; }
}
