using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.DataDumps.UploadDataDump;

public class UploadDataDumpCommandModel
{
    [RegularExpression(Pattern.Common.AnythingMultiLine)]
    [Required(ErrorMessage = "Något innehåll krävs.")]
    [StringLength(MaxLengths.Domain.DataDump.JsonData)]
    public string JsonData { get; set; }
}
