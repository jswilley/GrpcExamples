using Microsoft.EntityFrameworkCore;
using TestingWASM.Shared.Data.Entities;

namespace TestingWASM.Shared.Data.Mapping
{
    public partial class FormSectionMap
        : IEntityTypeConfiguration<FormSection>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FormSection> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("FormSection", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasField("ID")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasField("Name")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.AddTimeStamp)
                .IsRequired()
                .HasField("AddTimeStamp")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(t => t.AddUser)
                .IsRequired()
                .HasField("AddUser")
                //.HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .HasDefaultValueSql("('convert')");

            builder.Property(t => t.ChangeTimeStamp)
                .IsRequired()
                .HasField("ChangeTimeStamp")
                .HasColumnType("datetime")
               .HasDefaultValueSql("(getdate())");

            builder.Property(t => t.ChangeUser)
                .IsRequired()
                .HasField("ChangeUser")
                //.HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .HasDefaultValueSql("('convert')");

            // relationships
            #endregion
        }

    }
}
