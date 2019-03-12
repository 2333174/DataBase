namespace DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GymDB : DbContext
    {
        public GymDB()
            : base("name=GymDB")
        {
        }

        public virtual DbSet<Athlete> athlete { get; set; }
        public virtual DbSet<Judge> judge { get; set; }
        public virtual DbSet<Login> login { get; set; }
        public virtual DbSet<MatchGroup> matchgroup { get; set; }
        public virtual DbSet<PersonalResult> personalresult { get; set; }
        public virtual DbSet<Staff> staff { get; set; }
        public virtual DbSet<Team> team { get; set; }
        public virtual DbSet<TeamResult> teamresult { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Athlete>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Athlete>()
                .Property(e => e.IDNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Athlete>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<Athlete>()
                .Property(e => e.AthleteID)
                .IsUnicode(false);

            modelBuilder.Entity<Athlete>()
                .HasMany(e => e.personalresult)
                .WithRequired(e => e.Athlete)
                .HasForeignKey(e => e.AthleteID);

            modelBuilder.Entity<Judge>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Judge>()
                .Property(e => e.IDNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Judge>()
                .Property(e => e.Telephone)
                .IsUnicode(false);

            modelBuilder.Entity<Login>()
                .Property(e => e.UName)
                .IsUnicode(false);

            modelBuilder.Entity<Login>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<MatchGroup>()
                .Property(e => e.GroupID)
                .IsUnicode(false);

            modelBuilder.Entity<PersonalResult>()
                .Property(e => e.AthleteID)
                .IsUnicode(false);

            modelBuilder.Entity<PersonalResult>()
                .Property(e => e.SportsEvent)
                .IsUnicode(false);

            modelBuilder.Entity<PersonalResult>()
                .Property(e => e.Groupid)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.IDNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Telephone)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.TName)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.athlete)
                .WithOptional(e => e.team)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TeamResult>()
                .Property(e => e.Event)
                .IsUnicode(false);
        }
    }
}
