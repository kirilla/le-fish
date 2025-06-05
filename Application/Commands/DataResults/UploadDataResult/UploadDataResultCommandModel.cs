using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.DataResults.UploadDataResult;

public class UploadDataResultCommandModel
{
    [RegularExpression(Pattern.Common.AnythingMultiLine)]
    [Required(ErrorMessage = "Något innehåll krävs.")]
    [StringLength(MaxLengths.Domain.DataResult.JsonData)]
    public string JsonData { get; set; }

    public string? Url { get; set; }
    public string? Method { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
}
