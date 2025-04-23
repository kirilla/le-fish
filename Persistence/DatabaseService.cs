using Lefish.Persistence.Configuration;

namespace Lefish.Persistence;

public class DatabaseService(
    DbContextOptions<DatabaseService> options,
    IOnSaveFormatter formatter,
    IOnSaveValidator validator,
    ICreatedDateTimeSetter createdDateTimeSetter,
    IUpdatedDateTimeSetter updatedDateTimeSetter) : DbContext(options), IDatabaseService
{
    public DbSet<EmailAccount> EmailAccounts { get; set; }
    public DbSet<EmailAttachment> EmailAttachments { get; set; }
    public DbSet<EmailImage> EmailImages { get; set; }
    public DbSet<EmailMessage> EmailMessages { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<EmailTarget> EmailTargets { get; set; }
    public DbSet<EmailTemplate> EmailTemplates { get; set; }
    public DbSet<PageVisit> PageVisits { get; set; }
    public DbSet<PayloadPage> PayloadPages { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserEmail> UserEmails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        new EmailAccountConfiguration().Configure(builder.Entity<EmailAccount>());
        new EmailAttachmentConfiguration().Configure(builder.Entity<EmailAttachment>());
        new EmailImageConfiguration().Configure(builder.Entity<EmailImage>());
        new EmailMessageConfiguration().Configure(builder.Entity<EmailMessage>());
        new SessionConfiguration().Configure(builder.Entity<Session>());
        new EmailTargetConfiguration().Configure(builder.Entity<EmailTarget>());
        new EmailTemplateConfiguration().Configure(builder.Entity<EmailTemplate>());
        new PageVisitConfiguration().Configure(builder.Entity<PageVisit>());
        new PayloadPageConfiguration().Configure(builder.Entity<PayloadPage>());
        new UserConfiguration().Configure(builder.Entity<User>());
        new UserEmailConfiguration().Configure(builder.Entity<UserEmail>());
    }

    public async Task SaveAsync(IUserToken userToken)
    {
        if (!ChangeTracker.HasChanges())
            return;

        createdDateTimeSetter.SetCreated(ChangeTracker);
        updatedDateTimeSetter.SetUpdated(ChangeTracker);

        formatter.Format(ChangeTracker);
        validator.Validate(ChangeTracker);

        await base.SaveChangesAsync();

        if (ChangeTracker.HasChanges())
        {
            await base.SaveChangesAsync();
        }
    }
}
