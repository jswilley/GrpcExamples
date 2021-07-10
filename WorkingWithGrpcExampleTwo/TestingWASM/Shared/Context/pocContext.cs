using TestingWASM.Shared.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TestingWASM.Shared.Context
{
    public interface IpocContext
    {
        DbSet<FormQuestion> FormQuestions { get; set; }
        DbSet<FormSection> FormSections { get; set; }
        DbSet<FormType> FormTypes { get; set; }
        DbSet<FollowUpQuestion> FollowUpQuestions { get; set; }
        DbSet<QuestionResponseOptionFollowUpQuestionMap> QuestionResponseOptionFollowUpQuestionMaps { get; set; }
        DbSet<QuestionResponseOption> QuestionResponseOptions { get; set; }
        DbSet<QuestionResponse> QuestionResponses { get; set; }
        DbSet<Question> Questions { get; set; }
       // DbSet<User> Users { get; set; }

        int SaveChanges();
    }

    public partial class pocContext : DbContext, IpocContext
    {
        public pocContext(DbContextOptions<pocContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //     optionsBuilder.UseSqlServer("Data Source=mcsossdbdev.mcso.mccj.local\\dbdev,2518;Initial Catalog=SO_poc;Trusted_Connection=True;MultipleActiveResultSets=True;");
        }



        public virtual DbSet<FollowUpQuestion> FollowUpQuestions { get; set; }
        public virtual DbSet<FormEntry> FormEntries { get; set; }
        public virtual DbSet<FormFollowupQuestionEntry> FormFollowupQuestionEntries { get; set; }
        public virtual DbSet<FormQuestion> FormQuestions { get; set; }
        public virtual DbSet<FormQuestionEntry> FormQuestionEntries { get; set; }
        public virtual DbSet<FormSection> FormSections { get; set; }
        public virtual DbSet<FormType> FormTypes { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionResponse> QuestionResponses { get; set; }
        public virtual DbSet<QuestionResponseOption> QuestionResponseOptions { get; set; }
        public virtual DbSet<QuestionResponseOptionFollowUpQuestionMap> QuestionResponseOptionFollowUpQuestionMaps { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FollowUpQuestion>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.Property(e => e.DefaultResponse).IsUnicode(false);

                entity.Property(e => e.Prompt).IsUnicode(false);

                entity.HasOne(d => d.QuestionResponse)
                    .WithMany(p => p.FollowUpQuestions)
                    .HasForeignKey(d => d.QuestionResponseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FollowUpQuestion_QuestionResponse");
            });

            modelBuilder.Entity<FormEntry>(entity =>
            {
                entity.Property(e => e.AddTimeStamp).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AddUser).HasDefaultValueSql("('convert')");

                entity.Property(e => e.ChangeTimeStamp).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ChangeUser).HasDefaultValueSql("('convert')");

                entity.HasOne(d => d.FormType)
                    .WithMany(p => p.FormEntries)
                    .HasForeignKey(d => d.FormTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormEntry_FormType");
            });

            modelBuilder.Entity<FormFollowupQuestionEntry>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.FollowUpQuestion)
                    .WithMany(p => p.FormFollowupQuestionEntries)
                    .HasForeignKey(d => d.FollowUpQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormFollowupQuestionEntry_FollowUpQuestion");

                entity.HasOne(d => d.FormQuestionEntry)
                    .WithMany(p => p.FormFollowupQuestionEntries)
                    .HasForeignKey(d => d.FormQuestionEntryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormFollowupQuestionEntry_FormQuestionEntry");
            });

            modelBuilder.Entity<FormQuestion>(entity =>
            {
                entity.HasIndex(e => e.QuestionId)
                    .HasName("IX_FormQuestion");

                entity.Property(e => e.AddTimeStamp).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AddUser).HasDefaultValueSql("('convert')");

                entity.Property(e => e.ChangeTimeStamp).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ChangeUser).HasDefaultValueSql("('convert')");

                entity.Property(e => e.EndDate).HasDefaultValueSql("(dateadd(year,(50),getdate()))");

                entity.Property(e => e.Required).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FormType)
                    .WithMany(p => p.FormQuestions)
                    .HasForeignKey(d => d.FormTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormQuestion_FormType");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.FormQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormQuestion_Question");

                entity.HasOne(d => d.QuestionResponse)
                    .WithMany(p => p.FormQuestions)
                    .HasForeignKey(d => d.QuestionResponseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormQuestion_QuestionResponse");
            });

            modelBuilder.Entity<FormQuestionEntry>(entity =>
            {
                entity.Property(e => e.AddTimeStamp).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AddUser).HasDefaultValueSql("('convert')");

                entity.Property(e => e.ChangeTimeStamp).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ChangeUser).HasDefaultValueSql("('convert')");

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.FormQuestionEntries)
                    .HasForeignKey(d => d.FormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormQuestionEntry_FormEntry");

                entity.HasOne(d => d.FormQuestion)
                    .WithMany(p => p.FormQuestionEntries)
                    .HasForeignKey(d => d.FormQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormQuestionEntry_FormQuestion");
            });

            modelBuilder.Entity<FormSection>(entity =>
            {
                entity.Property(e => e.AddTimeStamp).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AddUser).HasDefaultValueSql("('convert')");

                entity.Property(e => e.ChangeTimeStamp).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ChangeUser).HasDefaultValueSql("('convert')");
            });

            modelBuilder.Entity<FormType>(entity =>
            {
                entity.Property(e => e.AddTimeStamp).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AddUser).HasDefaultValueSql("('convert')");

                entity.Property(e => e.ChangeTimeStamp).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ChangeUser).HasDefaultValueSql("('convert')");

                entity.Property(e => e.EndDate).HasDefaultValueSql("(dateadd(year,(50),getdate()))");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IX_Question")
                    .IsUnique();

                entity.Property(e => e.AddTimeStamp).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AddUser).HasDefaultValueSql("('convert')");

                entity.Property(e => e.ChangeTimeStamp).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ChangeUser).HasDefaultValueSql("('convert')");

                entity.Property(e => e.EndDate).HasDefaultValueSql("(dateadd(year,(50),getdate()))");
            });

            modelBuilder.Entity<QuestionResponse>(entity =>
            {
                entity.Property(e => e.AddTimeStamp).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AddUser).HasDefaultValueSql("('convert')");

                entity.Property(e => e.ChangeTimeStamp).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ChangeUser).HasDefaultValueSql("('convert')");
            });

            modelBuilder.Entity<QuestionResponseOption>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.Property(e => e.LongDescription).IsUnicode(false);

                entity.Property(e => e.ShortDescription).IsUnicode(false);

                entity.Property(e => e.SortOrder).HasDefaultValueSql("((200))");

                entity.HasOne(d => d.QuestionResponse)
                    .WithMany(p => p.QuestionResponseOptions)
                    .HasForeignKey(d => d.QuestionResponseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionResponseOption_QuestionResponse");
            });

            modelBuilder.Entity<QuestionResponseOptionFollowUpQuestionMap>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.Property(e => e.Required).HasDefaultValueSql("((1))");

                entity.Property(e => e.SortOrder).HasDefaultValueSql("((200))");

                entity.HasOne(d => d.FollowUpQuestion)
                    .WithMany(p => p.QuestionResponseOptionFollowUpQuestionMaps)
                    .HasForeignKey(d => d.FollowUpQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionResponseOptionFollowUpQuestionMap_FollowUpQuestion");

                entity.HasOne(d => d.FormQuestion)
                    .WithMany(p => p.QuestionResponseOptionFollowUpQuestionMaps)
                    .HasForeignKey(d => d.FormQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionResponseOptionFollowUpQuestionMap_FormQuestion");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
