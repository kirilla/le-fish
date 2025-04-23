namespace Lefish.Domain.Entities;

public class User : ICreatedDateTime, IUpdatedDateTime
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string PasswordHash { get; set; }

    public Guid? Guid { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }

    // Shared
    public List<Session> Sessions { get; set; }
    public List<UserEmail> UserEmails { get; set; }
}
