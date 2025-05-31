using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.DataDumps.UploadDataDump;

public class UploadDataDumpCommandModel
{
    public byte[] Data { get; set; }

    //public int ContentLength { get; set; }

    [Required]
    [StringLength(MaxLengths.Domain.DataDump.Name)]
    public string Name { get; set; }

    [Required]
    [StringLength(MaxLengths.Domain.DataDump.ContentType)]
    public string ContentType { get; set; }
}
