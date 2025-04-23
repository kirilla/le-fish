using System.ComponentModel.DataAnnotations;
using Lefish.Common.Validation;

namespace Lefish.Application.Commands.PayloadPages.ClonePayloadPage;

public class ClonePayloadPageCommandModel
{
    public int PayloadPageId { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.PayloadPage.Name,
        ErrorMessage = "Skriv kortare.")]
    public string CloneName { get; set; }
}
