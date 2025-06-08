using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.WebPages.EditWebPage;

public class EditWebPageCommandModel
{
    public int Id { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.WebPage.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; }

    [RegularExpression(Pattern.Common.AnythingMultiLine)]
    [Required(ErrorMessage = "Skriv HTML.")]
    [StringLength(
        MaxLengths.Domain.WebPage.Html,
        ErrorMessage = "Skriv kortare.")]
    public string Html { get; set; }
}
