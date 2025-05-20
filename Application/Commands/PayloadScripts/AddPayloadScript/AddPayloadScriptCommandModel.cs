using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.PayloadScripts.AddPayloadScript;

public class AddPayloadScriptCommandModel
{
    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.PayloadScript.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; }

    [RegularExpression(Pattern.Common.AnythingMultiLine)]
    [Required(ErrorMessage = "Skriv ett skript.")]
    [StringLength(
        MaxLengths.Domain.PayloadScript.Script,
        ErrorMessage = "Skriv kortare.")]
    public string Script { get; set; }
}
