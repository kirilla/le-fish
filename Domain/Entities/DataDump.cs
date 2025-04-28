using System.ComponentModel.DataAnnotations;

namespace Lefish.Domain.Entities;

public class DataDump
{
    public int Id { get; set; }

    public byte[] Data { get; set; }

    public int ContentLength { get; set; }

    [Required]
    [StringLength(MaxLengths.Domain.DataDump.Name)]
    public string Name { get; set; }

    [Required]
    [StringLength(MaxLengths.Domain.DataDump.ContentType)]
    public string ContentType { get; set; }

    public int EmailTargetId { get; set; }
    public EmailTarget EmailTarget { get; set; }
}
