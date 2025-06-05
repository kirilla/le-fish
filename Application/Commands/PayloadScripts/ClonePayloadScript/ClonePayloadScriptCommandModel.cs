using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.PayloadScripts.ClonePayloadScript;

public class ClonePayloadScriptCommandModel
{
    public int PayloadScriptId { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.PageScript.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; }
}
