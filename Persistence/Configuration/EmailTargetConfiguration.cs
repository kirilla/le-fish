namespace Lefish.Persistence.Configuration;

class EmailTargetConfiguration : IEntityTypeConfiguration<EmailTarget>
{
    public void Configure(EntityTypeBuilder<EmailTarget> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailTarget.Name);

        builder.Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailTarget.Address);

        builder.Property(p => p.PersonKey)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailTarget.PersonKey);

        builder.HasMany(x => x.EmailMessages)
            .WithOne(x => x.EmailTarget)
            .HasForeignKey(x => x.EmailTargetId)
            .IsRequired(false);

        builder.HasMany(x => x.PageVisits)
            .WithOne(x => x.EmailTarget)
            .HasForeignKey(x => x.EmailTargetId);
    }
}
