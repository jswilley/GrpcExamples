using Microsoft.EntityFrameworkCore;
using TestingWASM.Shared.Data.Entities;

namespace TestingWASM.Shared.Data.Mapping
{
    public partial class FormQuestionMap
        : IEntityTypeConfiguration<FormQuestion>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FormQuestion> builder)
        {
            #region Generated Configure
            // table
           // builder..ToTable("FormQuestion", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasField("ID")
                //.HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.QuestionId)
                .IsRequired()
                .HasField("QuestionID");
            //.("int");

            builder.Property(t => t.FormTypeId)
                .IsRequired()
                .HasField("FormTypeId");
               //.HasColumnType("int");

            builder.Property(t => t.Required)
                .IsRequired()
                .HasField("Required")
                .ValueGeneratedOnAdd()
               // .HasColumnType("bit")
                ;

            builder.Property(t => t.FormSection)
                .HasField("FormSection")
             //   .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.QuestionResponseId)
                .IsRequired()
                .HasField("QuestionResponseID")
               .HasColumnType("int");

            builder.Property(t => t.Sequence)
                .IsRequired()
                .HasField("Sequence")
               .HasColumnType("int");

            builder.Property(t => t.EndDate)
                //.IsRequired()
                .HasField("EndDate");
               // .HasColumnType("datetime");
                //.HasDefaultValueSql("(dateadd(year,(50),getdate()))");

            builder.Property(t => t.AddTimeStamp)
                .IsRequired()
                .HasField("AddTimeStamp")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(t => t.AddUser)
                .IsRequired()
                .HasField("AddUser")
              //  //.HasColumnType("nvarchar(50)")
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
            builder.HasOne(t => t.FormType)
                .WithMany(t => t.FormQuestions)
                .HasForeignKey(d => d.FormTypeId);
            // .HasConstraintName("FK_FormQuestion_FormType");

            //builder.HasOne(t => t.ResponseQuestionResponse)
            //    .WithMany(t => t.ResponseFormQuestions)
            //    .HasForeignKey(d => d.QuestionResponseId);
            //// .HasConstraintName("FK_FormQuestion_QuestionResponse");

            //builder.HasOne(t => t.QuestionQuestions)
            //    .WithMany(t => t.QuestionFormQuestions)
            //    .HasForeignKey(d => d.QuestionId);
            //   // .HasConstraintName("FK_FormQuestion_Question");

            #endregion
        }

    }
}
