using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.PageScripts.EditPageScript;

public class EditPageScriptCommandModel
{
    public int Id { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.PageScript.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; }

    [RegularExpression(Pattern.Common.AnythingMultiLine)]
    [Required(ErrorMessage = "Skriv ett skript.")]
    [StringLength(
        MaxLengths.Domain.PageScript.Script,
        ErrorMessage = "Skriv kortare.")]
    public string Script { get; set; }
}
