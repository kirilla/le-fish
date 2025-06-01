using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.DataDumps.EditDataDump;

public class EditDataDumpCommandModel
{
    public int Id { get; set; }

    [RegularExpression(Pattern.Common.AnythingMultiLine)]
    [Required(ErrorMessage = "Något innehåll krävs.")]
    [StringLength(MaxLengths.Domain.DataDump.JsonData)]
    public string JsonData { get; set; }
}
