using Lefish.Common.Validation;
using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.EmailTemplates.AddEmailTemplate;

public class AddEmailTemplateCommandModel
{
    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange ämnesrad.")]
    [StringLength(
        MaxLengths.Domain.EmailTemplate.Subject,
        ErrorMessage = "Skriv kortare.")]
    public string Subject { get; set; }

    [RegularExpression(Pattern.Common.AnythingMultiLine)]
    [Required(ErrorMessage = "Skriv en HTML-kropp.")]
    [StringLength(
        MaxLengths.Domain.EmailTemplate.HtmlBody,
        ErrorMessage = "Skriv kortare.")]
    public string HtmlBody { get; set; }

    [RegularExpression(Pattern.Common.AnythingMultiLine)]
    [Required(ErrorMessage = "Skriv en text-version.")]
    [StringLength(
        MaxLengths.Domain.EmailTemplate.TextBody,
        ErrorMessage = "Skriv kortare.")]
    public string TextBody { get; set; }
}
