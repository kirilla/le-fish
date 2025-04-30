using System.ComponentModel.DataAnnotations;
using Lefish.Common.Validation;

namespace Lefish.Application.Commands.PayloadPages.ClonePayloadPage;

public class ClonePayloadPageCommandModel
{
    public int PayloadPageId { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange phishing-nyckel.")]
    [StringLength(
        MaxLengths.Domain.PayloadPage.PageKey,
        ErrorMessage = "Skriv kortare.")]
    public string PageKey { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [StringLength(
        MaxLengths.Domain.PayloadPage.Comment,
        ErrorMessage = "Skriv kortare.")]
    public string? Comment { get; set; }
}
