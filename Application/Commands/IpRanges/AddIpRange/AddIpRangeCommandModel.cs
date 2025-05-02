using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.IpRanges.AddIpRange;

public class AddIpRangeCommandModel
{
    [Required(ErrorMessage = "Skriv ett adressintervall.")]
    [StringLength(
        MaxLengths.Common.IpAddress.IPv6,
        ErrorMessage = "Skriv kortare.")]
    public string Range { get; set; }

    public bool Blocked { get; set; }
}
