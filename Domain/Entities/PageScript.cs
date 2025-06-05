
namespace Lefish.Domain.Entities;

public class PageScript : ICreatedDateTime
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Script { get; set; }

    public DateTime? Created { get; set; }

    public List<Attack> Attacks { get; set; }
}
