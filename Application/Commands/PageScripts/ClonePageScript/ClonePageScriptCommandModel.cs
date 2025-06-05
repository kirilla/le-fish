using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.PageScripts.ClonePageScript;

public class ClonePageScriptCommandModel
{
    public int PayloadScriptId { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.PageScript.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; }
}
