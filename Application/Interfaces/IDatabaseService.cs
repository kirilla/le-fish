using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lefish.Application.Interfaces;

public interface IDatabaseService
{
    DbSet<Attack> Attacks { get; set; }
    DbSet<AttackEvent> AttackEvents { get; set; }
    DbSet<BlockedRequest> BlockedRequests { get; set; }
    DbSet<DataResult> DataResults { get; set; }
    DbSet<EmailAccount> EmailAccounts { get; set; }
    DbSet<EmailAttachment> EmailAttachments { get; set; }
    DbSet<EmailImage> EmailImages { get; set; }
    DbSet<EmailMessage> EmailMessages { get; set; }
    DbSet<EmailTemplate> EmailTemplates { get; set; }
    DbSet<Instruction> Instructions { get; set; }
    DbSet<IpRange> IpRanges { get; set; }
    DbSet<PayloadPage> PayloadPages { get; set; }
    DbSet<PayloadScript> PayloadScripts { get; set; }
    DbSet<Session> Sessions { get; set; }
    DbSet<Target> Targets { get; set; }
    DbSet<TargetInstruction> TargetInstructions { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<UserEmail> UserEmails { get; set; }
    
    Task SaveAsync(IUserToken userToken);

    ChangeTracker ChangeTracker { get; }
}
