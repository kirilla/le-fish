
namespace Lefish.Domain.Entities;

public class WebPage : ICreatedDateTime
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Html { get; set; }

    public DateTime? Created { get; set; }

    public List<Attack> Attacks { get; set; }
}
