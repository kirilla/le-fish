using System.ComponentModel.DataAnnotations;
using Lefish.Common.Validation;

namespace Lefish.Application.Commands.PayloadPages.EditPayloadPage;

public class EditPayloadPageCommandModel
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

    [RegularExpression(Pattern.Common.AnythingMultiLine)]
    [Required(ErrorMessage = "Skriv HTML.")]
    [StringLength(
        MaxLengths.Domain.PayloadPage.Html,
        ErrorMessage = "Skriv kortare.")]
    public string Html { get; set; }
}
