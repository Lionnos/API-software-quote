using appservicequotes.DataAccess.Entity;
using appservicequotes.Generic;
using Microsoft.EntityFrameworkCore;

namespace appservicequotes.DataAccess.Connection
{
    public class DataBaseContext : DataBaseContextGeneric
    {
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.Relational().TableName = entityType.DisplayName();
            }

            modelBuilder.Entity<User>().ToTable("tUser");
            modelBuilder.Entity<Professional>().ToTable("tProfessional");
            modelBuilder.Entity<ProjectType>().ToTable("tProjecType");
            modelBuilder.Entity<ProjectTypeMechanism>().ToTable("tProjectTypeMechanism");
            modelBuilder.Entity<AssignProject>().ToTable("tAssignProject ");

            base.OnModelCreating(modelBuilder);
        }
        */

        public DbSet<User> Users { get; set; }
        public DbSet<Professional> Professionals { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<ProjectTypeMechanism> ProjectTypeMechanisms { get; set; }
        public DbSet<AssignProject> AssignProjects { get; set; }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseSqlServer(AppSettings.GetConnectionStringSqlServer());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("tUser");
            modelBuilder.Entity<Professional>().ToTable("tProfessional");
            modelBuilder.Entity<ProjectType>().ToTable("tProjectType");
            modelBuilder.Entity<ProjectTypeMechanism>().ToTable("tProjectTypeMechanism");
            modelBuilder.Entity<AssignProject>().ToTable("tAssignProject");

            base.OnModelCreating(modelBuilder);

            //  ================================ RELACION DE MODELOS
            modelBuilder.Entity<Professional>()
                .HasMany(e => e.childAssignProject)
                .WithOne(e => e.Professional)
                .HasForeignKey(e => e.idAssignProject)
                .IsRequired();

            modelBuilder.Entity<ProjectType>()
                .HasMany(e => e.childProjectTypeMechanism)
                .WithOne(e => e.ProjectType)
                .HasForeignKey(e => e.idProjectType)
                .IsRequired();

            modelBuilder.Entity<ProjectTypeMechanism>()
                .HasMany(e => e.childAssignProject)
                .WithOne(e => e.ProjectTypeMechanism)
                .HasForeignKey(e => e.idProjectTypeMechanism)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                //optionsBuilder.UseSqlServer("Server=Acer\\SQLEXPRESS; Database=dbSistemaCotizaciones; Trusted_Connection=True; TrustServerCertificate=True;");
                optionsBuilder.UseSqlServer("Server=Acer\\SQLEXPRESS; Database=dbSistemaCotizaciones; User Id=Henry; Password=1001sqlserver; TrustServerCertificate=True;");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
