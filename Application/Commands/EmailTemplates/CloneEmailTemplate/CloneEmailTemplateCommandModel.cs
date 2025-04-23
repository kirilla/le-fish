using System.ComponentModel.DataAnnotations;
using Lefish.Common.Validation;

namespace Lefish.Application.Commands.EmailTemplates.CloneEmailTemplate;

public class CloneEmailTemplateCommandModel
{
    public int EmailTemplateId { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange ämnesrad.")]
    [StringLength(
        MaxLengths.Domain.EmailTemplate.Subject,
        ErrorMessage = "Skriv kortare.")]
    public string CloneSubject { get; set; }
}
