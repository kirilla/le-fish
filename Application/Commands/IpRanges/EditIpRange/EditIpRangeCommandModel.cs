using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.IpRanges.EditIpRange;

public class EditIpRangeCommandModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Skriv ett adressintervall.")]
    [StringLength(
        MaxLengths.Common.IpAddress.IPv6,
        ErrorMessage = "Skriv kortare.")]
    public string Range { get; set; }

    public bool Blocked { get; set; }
}
