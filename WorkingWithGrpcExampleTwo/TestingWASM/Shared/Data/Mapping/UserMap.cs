using poc.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace poc.Core.Data.Mapping
{
    public partial class UserMap
        : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("User", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasField("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Created)
                .IsRequired()
                .HasField("Created")
                .HasColumnType("datetime2");

            builder.Property(t => t.Modified)
                .IsRequired()
                .HasField("Modified")
                .HasColumnType("datetime2");

            builder.Property(t => t.FirstName)
                .IsRequired()
                .HasField("FirstName")
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20);

            builder.Property(t => t.LastName)
                .IsRequired()
                .HasField("LastName")
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20);

            builder.Property(t => t.UserName)
                .HasField("UserName")
                .HasColumnType("nvarchar(30)")
                .HasMaxLength(30);

            builder.Property(t => t.Email)
                .IsRequired()
                .HasField("Email")
                .HasColumnType("nvarchar(30)")
                .HasMaxLength(30);

            builder.Property(t => t.Description)
                .HasField("Description")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);

            builder.Property(t => t.IsAdminRole)
                .IsRequired()
                .HasField("IsAdminRole")
                .HasColumnType("bit");

            builder.Property(t => t.Roles)
                .HasField("Roles")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);

            builder.Property(t => t.IsActive)
                .IsRequired()
                .HasField("IsActive")
                .HasColumnType("bit");

            builder.Property(t => t.Password)
                .HasField("Password")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);

            builder.Property(t => t.AccountId)
                .IsRequired()
                .HasField("AccountId");
               //.HasColumnType("int");

            builder.Property(t => t.RowVersion)
                .IsRowVersion()
                .HasField("RowVersion")
                .HasColumnType("rowversion")
                .HasMaxLength(8)
                .ValueGeneratedOnAddOrUpdate();

            // relationships
            #endregion
        }

    }
}
