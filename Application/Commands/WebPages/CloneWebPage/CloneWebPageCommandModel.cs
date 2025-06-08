using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.WebPages.CloneWebPage;

public class CloneWebPageCommandModel
{
    public int Id { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.WebPage.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; }
}
