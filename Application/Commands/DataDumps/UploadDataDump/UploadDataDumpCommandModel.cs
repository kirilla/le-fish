using System.ComponentModel.DataAnnotations;
using Lefish.Common.Validation;

namespace Lefish.Application.Commands.DataDumps.UploadDataDump;

public class UploadDataDumpCommandModel
{
    public int EmailTargetId { get; set; }

    public byte[] Data { get; set; }

    //public int ContentLength { get; set; }

    [Required]
    [StringLength(MaxLengths.Domain.DataDump.Name)]
    public string Name { get; set; }

    [Required]
    [StringLength(MaxLengths.Domain.DataDump.ContentType)]
    public string ContentType { get; set; }
}
