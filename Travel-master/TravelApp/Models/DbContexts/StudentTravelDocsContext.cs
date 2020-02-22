using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
//using TravelApp.Models.DbTables;

namespace TravelApp.Models
{
    public partial class StudentTravelDocsContext : DbContext
    {
        public StudentTravelDocsContext()
        {
        }

        public StudentTravelDocsContext(DbContextOptions<StudentTravelDocsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AcademicStatus> AcademicStatus { get; set; }
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<ApplicationForms> ApplicationForms { get; set; }
        public virtual DbSet<ApplicationStatus> ApplicationStatus { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Destination> Destination { get; set; }
        public virtual DbSet<Flight> Flight { get; set; }
        public virtual DbSet<Form> Form { get; set; }
        public virtual DbSet<FormStatus> FormStatus { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Program> Program { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Trip> Trip { get; set; }
        public virtual DbSet<TripRoster> TripRoster { get; set; }
        public virtual DbSet<TripQuestion> TripQuestion { get; set; }
        public virtual DbSet<TripQuestionResponse> TripQuestionResponse { get; set;  }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=bitsql.wctc.edu;Database=StudentTravelDocs;User ID=StudentTravelApp;Password=Tr@v3ler;Trusted_Connection=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcademicStatus>(entity =>
            {
                entity.Property(e => e.AcademicStatusId).HasColumnName("AcademicStatusID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.AcademicStatusId).HasColumnName("AcademicStatusID");

                entity.Property(e => e.AppStatusId)
                    .HasColumnName("AppStatusID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ApplicationDate).HasColumnType("datetime");

                entity.Property(e => e.Gpa)
                    .HasColumnName("GPA")
                    .HasColumnType("numeric(3, 2)");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Rationale).IsUnicode(false);

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasColumnName("StudentID")
                    .HasMaxLength(450);

                entity.Property(e => e.TripId).HasColumnName("TripID");

                entity.HasOne(d => d.AcademicStatus)
                    .WithMany(p => p.Application)
                    .HasForeignKey(d => d.AcademicStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Application_AcademicStatusID");

                entity.HasOne(d => d.AppStatus)
                    .WithMany(p => p.Application)
                    .HasForeignKey(d => d.AppStatusId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Application_AppStatusID");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Application)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("fk_Application_StudentID");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.Application)
                    .HasForeignKey(d => d.TripId)
                    .HasConstraintName("fk_Application_TripID");
            });

            modelBuilder.Entity<ApplicationForms>(entity =>
            {
                entity.HasKey(e => e.ApplicationFormId);

                entity.Property(e => e.ApplicationFormId).HasColumnName("ApplicationFormID");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.FormId).HasColumnName("FormID");

                entity.Property(e => e.FormStatusId)
                    .HasColumnName("FormStatusID")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.ApplicationForms)
                    .HasForeignKey(d => d.ApplicationId)
                    .HasConstraintName("fk_ApplicationForms_ApplicationID");

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.ApplicationForms)
                    .HasForeignKey(d => d.FormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ApplicationForms_FormID");

                entity.HasOne(d => d.FormStatus)
                    .WithMany(p => p.ApplicationForms)
                    .HasForeignKey(d => d.FormStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ApplicationForms_FormStatusID");
            });

            modelBuilder.Entity<ApplicationStatus>(entity =>
            {
                entity.HasKey(e => e.AppStatusId);

                entity.Property(e => e.AppStatusId).HasColumnName("AppStatusID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Gender).HasMaxLength(1);

                entity.Property(e => e.LockoutEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.Property(e => e.WctcId).HasColumnName("WctcID");
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.CourseTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Crn)
                    .IsRequired()
                    .HasColumnName("CRN")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Destination>(entity =>
            {
                entity.Property(e => e.DestinationId).HasColumnName("DestinationID");

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.Property(e => e.FlightId).HasColumnName("FlightID");

                entity.Property(e => e.Airline)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.AirportCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.DepartDate).HasColumnType("date");

                entity.Property(e => e.FlightNumber)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ReturnDate).HasColumnType("date");
            });

            modelBuilder.Entity<Form>(entity =>
            {
                entity.Property(e => e.FormId).HasColumnName("FormID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FormStatus>(entity =>
            {
                entity.Property(e => e.FormStatusId).HasColumnName("FormStatusID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasColumnName("StudentID")
                    .HasMaxLength(450);

                entity.Property(e => e.TripId).HasColumnName("TripID");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("fk_Payment_StudentID");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Payment_TripID");
            });

            modelBuilder.Entity<Program>(entity =>
            {
                entity.Property(e => e.ProgramId).HasColumnName("ProgramID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentId)
                    .HasColumnName("StudentID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.ExpectedGradMonth)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ProgramId).HasColumnName("ProgramID");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.ProgramId)
                    .HasConstraintName("fk_Student_ProgramID");

                entity.HasOne(d => d.StudentNavigation)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.StudentId)
                    .HasConstraintName("fk_Student_AspNetUserId");
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.Property(e => e.TripId).HasColumnName("TripID");

                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CostToDistrict).HasColumnType("money");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateDeparting).HasColumnType("date");

                entity.Property(e => e.DateReturning).HasColumnType("date");

                entity.Property(e => e.DestinationId).HasColumnName("DestinationID");

                entity.Property(e => e.EducationalObjective)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.FlightId).HasColumnName("FlightID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RecommendationNeeded).HasDefaultValueSql("((0))");

                entity.Property(e => e.StudentOrganization)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.TeachingSubNeeded).HasDefaultValueSql("((0))");

                entity.Property(e => e.TransportationId).HasColumnName("TransportationID");

                entity.Property(e => e.TripApproved).HasDefaultValueSql("((0))");

                entity.Property(e => e.TripName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Trip)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Trip_CourseID");

                entity.HasOne(d => d.Destination)
                    .WithMany(p => p.Trip)
                    .HasForeignKey(d => d.DestinationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Trip_DestinationID");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.Trip)
                    .HasForeignKey(d => d.FlightId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Trip_FlightID");
            });

            modelBuilder.Entity<TripRoster>(entity =>
            {
                entity.Property(e => e.TripRosterId).HasColumnName("TripRosterID");

                entity.Property(e => e.PersonId)
                    .IsRequired()
                    .HasColumnName("PersonID")
                    .HasMaxLength(450);

                entity.Property(e => e.TripId).HasColumnName("TripID");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.TripRoster)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("fk_TripRoster_ASPNetUsers");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.TripRoster)
                    .HasForeignKey(d => d.TripId)
                    .HasConstraintName("fk_TripRoster_TripID");
            });
        }
    }
}
