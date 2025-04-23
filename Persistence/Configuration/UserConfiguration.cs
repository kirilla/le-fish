namespace Lefish.Persistence.Configuration;

class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(MaxLengths.Common.Person.Name);

        builder.Property(p => p.PasswordHash)
            .IsRequired()
            .HasMaxLength(MaxLengths.Common.Password.Hash);

        builder.HasMany(x => x.Sessions)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.UserEmails)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
    }
}
