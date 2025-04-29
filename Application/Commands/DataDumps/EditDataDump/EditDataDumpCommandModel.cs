using System.ComponentModel.DataAnnotations;
using Lefish.Common.Validation;

namespace Lefish.Application.Commands.DataDumps.EditDataDump;

public class EditDataDumpCommandModel
{
    public int DataDumpId { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange filnamn.")]
    [StringLength(MaxLengths.Domain.DataDump.Name)]
    public string Name { get; set; }

    [RegularExpression(Pattern.Common.Mime.Type)]
    [Required(ErrorMessage = "Ange filtyp.")]
    [StringLength(MaxLengths.Domain.DataDump.ContentType)]
    public string ContentType { get; set; }
}
