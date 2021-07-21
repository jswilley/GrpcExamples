using Microsoft.EntityFrameworkCore;
using Example2.Shared.Data.Entities;

namespace Example2.Shared.Context
{
    public interface IpocContext
    {
        DbSet<FormQuestion> FormQuestions { get; set; }
        DbSet<FormSection> FormSections { get; set; }
        DbSet<FormType> FormTypes { get; set; }

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


        public virtual DbSet<FormEntry> FormEntries { get; set; }

        public virtual DbSet<FormQuestion> FormQuestions { get; set; }
        public virtual DbSet<FormQuestionEntry> FormQuestionEntries { get; set; }
        public virtual DbSet<FormSection> FormSections { get; set; }
        public virtual DbSet<FormType> FormTypes { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionResponse> QuestionResponses { get; set; }
        public virtual DbSet<QuestionResponseOption> QuestionResponseOptions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {


                modelBuilder.Entity<FormType>(entity =>
                {
                    entity.HasKey(e => e.Id);


                });


                modelBuilder.Entity<FormEntry>(entity =>
                {
                    entity.HasOne(d => d.FormType)
                        .WithMany(p => p.FormEntries)
                        .HasForeignKey(d => d.FormTypeId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        ;
                });



                //modelBuilder.Entity<FormType>().HasData(new {
                //    Id = 1,
                //    Title = "Hope Team",
                //    ShortName = "Hope",
                //    EndDate = System.DateTime.Parse("2049-01-01T00:00:00"),
                //    SortOrder = 1,
                //    AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                //    AddUser = "seed",
                //    ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                //    ChangeUser = "seed"
                //});

                //modelBuilder.Entity<FormType>().OwnsMany(a => a.FormEntries).HasData(
                //    {
                //    new { }
                //})

                modelBuilder.Entity<FormEntry>().HasData(
                 DataSeed.GenFormEntry());

                modelBuilder.Entity<FormQuestionEntry>(entity =>
                {
                    entity.HasOne(d => d.Form)
                        .WithMany(p => p.FormQuestionEntries)
                        .HasForeignKey(d => d.FormId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                      ;

                    entity.HasOne(d => d.FormQuestion)
                        .WithMany(p => p.FormQuestionEntries)
                        .HasForeignKey(d => d.FormQuestionId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                      ;
                });

                //modelBuilder.Entity<FormQuestionEntry>().HasData(
                //    new
                //    {
                //        AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                //        AddUser = "seed",
                //        ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                //        ChangeUser = "seed",
                //        FormId = 1,
                //        Id = 1,
                //        ResponseValue = string.Empty,
                //        FormQuestionId = 0
                //    });


                modelBuilder.Entity<FormQuestion>(entity =>
                {
                    entity.HasIndex(e => e.QuestionId)
                      ;

                    entity.HasOne(d => d.FormType)
                        .WithMany(p => p.FormQuestions)
                        .HasForeignKey(d => d.FormTypeId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        ;

                    entity.HasOne(d => d.Question)
                        .WithMany(p => p.FormQuestions)
                        .HasForeignKey(d => d.QuestionId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        ;

                    entity.HasOne(d => d.QuestionResponse)
                        .WithMany(p => p.FormQuestions)
                        .HasForeignKey(d => d.QuestionResponseId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        ;
                });

                //modelBuilder.Entity<FormQuestion>().HasData(
                //    new
                //    {
                //        Id = 1,

                //        Required = false,
                //        FormSection = "Meetings",
                //        Sequence = 1,
                //        AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                //        AddUser = "seed",
                //        ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                //        ChangeUser = "seed",

                //        EndDate = System.DateTime.Parse("2077-12-30T00:00:00"),
                //        FormTypeId = 1,
                //        QuestionId = 1,
                //        QuestionResponseId = 5
                //    });

                //modelBuilder.Entity<FormQuestion>().HasData(
                // new
                // {
                //     Id = 2,

                //     Required = false,
                //     FormSection = "Meetings",
                //     Sequence = 2,
                //     AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                //     AddUser = "seed",
                //     ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                //     ChangeUser = "seed",

                //     EndDate = System.DateTime.Parse("2077-12-30T00:00:00"),
                //     FormTypeId = 1,

                //     QuestionId = 2,

                //     QuestionResponseId = 3
                // });

                //modelBuilder.Entity<FormQuestion>().HasData(
                //new
                //{
                //    Id = 3,
                //    Required = false,
                //    FormSection = "Meetings",
                //    Sequence = 3,
                //    AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                //    AddUser = "seed",
                //    ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                //    ChangeUser = "seed",
                //    EndDate = System.DateTime.Parse("2077-12-30T00:00:00"),
                //    FormTypeId = 1,

                //    QuestionId = 3,

                //    QuestionResponseId = 7
                //});

                //modelBuilder.Entity<FormQuestion>().HasData(
                //new
                //{
                //    Id = 4,

                //    Required = false,
                //    FormSection = "Meetings",
                //    Sequence = 4,
                //    AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                //    AddUser = "seed",
                //    ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                //    ChangeUser = "seed",

                //    EndDate = System.DateTime.Parse("2077-12-30T00:00:00"),
                //    FormTypeId = 1,

                //    QuestionId = 4,

                //    QuestionResponseId = 6
                //});

                modelBuilder.Entity<Question>(entity =>
                {
                    entity.HasIndex(e => e.Id)

                        .IsUnique();
                });
                //  modelBuilder.Entity<Question>().HasData(
                //      new
                //      {
                //          Id = 1,
                //          Text = "Meeting Type",
                //          EndDate = System.DateTime.Parse("Feb 21 2071 10:08AM"),
                //          AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                //          AddUser = "seed",
                //          ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                //          ChangeUser = "seed"
                //      });

                //  modelBuilder.Entity<Question>().HasData(
                //    new
                //    {
                //        Id = 2,
                //        Text = "Other Meeting Type",
                //        EndDate = System.DateTime.Parse("Feb 21 2071 10:08AM"),
                //        AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                //        AddUser = "seed",
                //        ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                //        ChangeUser = "seed"
                //    });

                //  modelBuilder.Entity<Question>().HasData(
                //  new
                //  {
                //      Id = 3,
                //      Text = "Meeting Date",
                //      EndDate = System.DateTime.Parse("Feb 21 2071 10:08AM"),
                //      AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                //      AddUser = "seed",
                //      ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                //      ChangeUser = "seed"
                //  });

                //  modelBuilder.Entity<Question>().HasData(
                //new
                //{
                //    Id = 4,
                //    Text = "Number of Attendess",
                //    EndDate = System.DateTime.Parse("Feb 21 2071 10:08AM"),
                //    AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                //    AddUser = "seed",
                //    ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                //    ChangeUser = "seed"
                //});

                modelBuilder.Entity<QuestionResponseOption>(entity =>
                {
                    entity.HasKey(e => e.Id)
                        ;

                    entity.Property(e => e.LongDescription).IsUnicode(false);

                    entity.Property(e => e.ShortDescription).IsUnicode(false);

                    entity.HasOne(d => d.QuestionResponse)
                        .WithMany(p => p.QuestionResponseOptions)
                        .HasForeignKey(d => d.QuestionResponseId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        ;
                });



                OnModelCreatingPartial(modelBuilder);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}