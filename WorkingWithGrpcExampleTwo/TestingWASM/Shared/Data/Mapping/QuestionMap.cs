using Microsoft.EntityFrameworkCore;
using TestingWASM.Shared.Data.Entities;

namespace TestingWASM.Shared.Data.Mapping
{
    public partial class QuestionMap
        : IEntityTypeConfiguration<Question>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Question> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Question", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasField("ID")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Text)
                .IsRequired()
                .HasField("Text")
                .HasColumnType("nvarchar(1000)")
                .HasMaxLength(1000);

            builder.Property(t => t.EndDate)
                .IsRequired()
                .HasField("EndDate")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(dateadd(year,(50),getdate()))");

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
