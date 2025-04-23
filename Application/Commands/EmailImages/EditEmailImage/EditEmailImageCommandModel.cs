using System.ComponentModel.DataAnnotations;
using Lefish.Common.Validation;

namespace Lefish.Application.Commands.EmailImages.EditEmailImage;

public class EditEmailImageCommandModel
{
    public int EmailImageId { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange filnamn.")]
    [StringLength(MaxLengths.Domain.EmailImage.Name)]
    public string Name { get; set; }

    [RegularExpression(Pattern.Common.Mime.Type)]
    [Required(ErrorMessage = "Ange filtyp.")]
    [StringLength(MaxLengths.Domain.EmailImage.ContentType)]
    public string ContentType { get; set; }
}
