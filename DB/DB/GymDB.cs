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

        public virtual DbSet<athlete> athlete { get; set; }
        public virtual DbSet<judge> judge { get; set; }
        public virtual DbSet<login> login { get; set; }
        public virtual DbSet<matchgroup> matchgroup { get; set; }
        public virtual DbSet<personalresult> personalresult { get; set; }
        public virtual DbSet<staff> staff { get; set; }
        public virtual DbSet<team> team { get; set; }
        public virtual DbSet<teamresult> teamresult { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<athlete>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<athlete>()
                .Property(e => e.IDNumber)
                .IsUnicode(false);

            modelBuilder.Entity<athlete>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<athlete>()
                .Property(e => e.AthleteID)
                .IsUnicode(false);

            modelBuilder.Entity<athlete>()
                .HasMany(e => e.personalresult)
                .WithRequired(e => e.athlete)
                .HasForeignKey(e => e.AthleteID);

            modelBuilder.Entity<judge>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<judge>()
                .Property(e => e.IDNumber)
                .IsUnicode(false);

            modelBuilder.Entity<judge>()
                .Property(e => e.Telephone)
                .IsUnicode(false);

            modelBuilder.Entity<login>()
                .Property(e => e.UName)
                .IsUnicode(false);

            modelBuilder.Entity<login>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<matchgroup>()
                .Property(e => e.GounpID)
                .IsUnicode(false);

            modelBuilder.Entity<personalresult>()
                .Property(e => e.AthleteID)
                .IsUnicode(false);

            modelBuilder.Entity<personalresult>()
                .Property(e => e.SportsEvent)
                .IsUnicode(false);

            modelBuilder.Entity<personalresult>()
                .Property(e => e.Gounpid)
                .IsUnicode(false);

            modelBuilder.Entity<staff>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<staff>()
                .Property(e => e.IDNumber)
                .IsUnicode(false);

            modelBuilder.Entity<staff>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<staff>()
                .Property(e => e.Telephone)
                .IsUnicode(false);

            modelBuilder.Entity<staff>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<team>()
                .Property(e => e.TName)
                .IsUnicode(false);

            modelBuilder.Entity<team>()
                .HasMany(e => e.athlete)
                .WithOptional(e => e.team)
                .WillCascadeOnDelete();

            modelBuilder.Entity<teamresult>()
                .Property(e => e.Event)
                .IsUnicode(false);
        }
    }
}
