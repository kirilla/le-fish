using Lefish.Persistence.Configuration;

namespace Lefish.Persistence;

public class DatabaseService(
    DbContextOptions<DatabaseService> options,
    IOnSaveFormatter formatter,
    IOnSaveValidator validator,
    ICreatedDateTimeSetter createdDateTimeSetter,
    IUpdatedDateTimeSetter updatedDateTimeSetter) : DbContext(options), IDatabaseService
{
    public DbSet<Attack> Attacks { get; set; }
    public DbSet<AttackEvent> AttackEvents { get; set; }
    public DbSet<BlockedRequest> BlockedRequests { get; set; }
    public DbSet<DataDump> DataDumps { get; set; }
    public DbSet<EmailAccount> EmailAccounts { get; set; }
    public DbSet<EmailAttachment> EmailAttachments { get; set; }
    public DbSet<EmailImage> EmailImages { get; set; }
    public DbSet<EmailMessage> EmailMessages { get; set; }
    public DbSet<EmailTarget> EmailTargets { get; set; }
    public DbSet<EmailTemplate> EmailTemplates { get; set; }
    public DbSet<Instruction> Instructions { get; set; }
    public DbSet<IpRange> IpRanges { get; set; }
    public DbSet<PayloadPage> PayloadPages { get; set; }
    public DbSet<PayloadScript> PayloadScripts { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<TargetInstruction> TargetInstructions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserEmail> UserEmails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        new AttackConfiguration().Configure(builder.Entity<Attack>());
        new VisitConfiguration().Configure(builder.Entity<AttackEvent>());
        new BlockedRequestConfiguration().Configure(builder.Entity<BlockedRequest>());
        new DataDumpConfiguration().Configure(builder.Entity<DataDump>());
        new EmailAccountConfiguration().Configure(builder.Entity<EmailAccount>());
        new EmailAttachmentConfiguration().Configure(builder.Entity<EmailAttachment>());
        new EmailImageConfiguration().Configure(builder.Entity<EmailImage>());
        new EmailMessageConfiguration().Configure(builder.Entity<EmailMessage>());
        new EmailTargetConfiguration().Configure(builder.Entity<EmailTarget>());
        new EmailTemplateConfiguration().Configure(builder.Entity<EmailTemplate>());
        new InstructionConfiguration().Configure(builder.Entity<Instruction>());
        new IpRangeConfiguration().Configure(builder.Entity<IpRange>());
        new PayloadPageConfiguration().Configure(builder.Entity<PayloadPage>());
        new PayloadScriptConfiguration().Configure(builder.Entity<PayloadScript>());
        new SessionConfiguration().Configure(builder.Entity<Session>());
        new TargetInstructionConfiguration().Configure(builder.Entity<TargetInstruction>());
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
