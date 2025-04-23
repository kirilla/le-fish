using System.ComponentModel.DataAnnotations;
using Lefish.Common.Validation;

namespace Lefish.Application.Commands.EmailTemplates.UploadEmailImage;

public class UploadEmailImageCommandModel
{
    public int EmailTemplateId { get; set; }

    public byte[] Data { get; set; }

    //public int ContentLength { get; set; }

    [Required]
    [StringLength(MaxLengths.Domain.EmailImage.Name)]
    public string Name { get; set; }

    [Required]
    [StringLength(MaxLengths.Domain.EmailImage.ContentType)]
    public string ContentType { get; set; }
}
