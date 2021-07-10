using Microsoft.EntityFrameworkCore;
using TestingWASM.Shared.Data.Entities;

namespace TestingWASM.Shared.Data.Mapping
{
    public partial class FormTypeMap
        : IEntityTypeConfiguration<FormType>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FormType> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("FormType", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasField("ID")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Title)
                .IsRequired()
                .HasField("Title")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.EndDate)
               .IsRequired()
               .HasField("EndDate")
               .HasColumnType("datetime")
               .HasDefaultValueSql("(dateadd(year,(50),getdate()))");

            //builder.Property(t => t.IntakeAssessmentRelated)
            //   .IsRequired()
            //   .HasField("IntakeAssessmentRelated")
            //   .HasColumnType("bit")
            //   .HasDefaultValueSql("((0))");

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
