using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lefish.Application.Interfaces;

public interface IDatabaseService
{
    DbSet<BlockedRequest> BlockedRequests { get; set; }
    DbSet<DataDump> DataDumps { get; set; }
    DbSet<EmailAccount> EmailAccounts { get; set; }
    DbSet<EmailAttachment> EmailAttachments { get; set; }
    DbSet<EmailImage> EmailImages { get; set; }
    DbSet<EmailMessage> EmailMessages { get; set; }
    DbSet<EmailTarget> EmailTargets { get; set; }
    DbSet<EmailTemplate> EmailTemplates { get; set; }
    DbSet<IpRange> IpRanges { get; set; }
    DbSet<PageVisit> PageVisits { get; set; }
    DbSet<PageToken> PageKeys { get; set; }
    DbSet<PayloadPage> PayloadPages { get; set; }
    DbSet<PayloadScript> PayloadScripts { get; set; }
    DbSet<ScriptVisit> ScriptVisits { get; set; }
    DbSet<Session> Sessions { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<UserEmail> UserEmails { get; set; }

    Task SaveAsync(IUserToken userToken);

    ChangeTracker ChangeTracker { get; }
}
