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
        public virtual DbSet<doctor> doctor { get; set; }
        public virtual DbSet<judge> judge { get; set; }
        public virtual DbSet<matchgroup> matchgroup { get; set; }
        public virtual DbSet<teamlogin> teamlogin { get; set; }
        public virtual DbSet<adminlogin> adminlogin { get; set; }
        public virtual DbSet<coach> coach { get; set; }
        public virtual DbSet<judgelogin> judgelogin { get; set; }
        public virtual DbSet<leader> leader { get; set; }
        public virtual DbSet<peasonalresult> peasonalresult { get; set; }
        public virtual DbSet<teamresult> teamresult { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<athlete>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<athlete>()
                .Property(e => e.idNumber)
                .IsUnicode(false);

            modelBuilder.Entity<athlete>()
                .Property(e => e.gender)
                .IsUnicode(false);

            modelBuilder.Entity<athlete>()
                .Property(e => e.teamName)
                .IsUnicode(false);

            modelBuilder.Entity<athlete>()
                .Property(e => e.athleteID)
                .IsUnicode(false);

            modelBuilder.Entity<athlete>()
                .HasMany(e => e.peasonalresult)
                .WithRequired(e => e.athlete)
                .HasForeignKey(e => e.playID);

            modelBuilder.Entity<doctor>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<doctor>()
                .Property(e => e.idNumber)
                .IsUnicode(false);

            modelBuilder.Entity<doctor>()
                .Property(e => e.telephone)
                .IsUnicode(false);

            modelBuilder.Entity<judge>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<judge>()
                .Property(e => e.idNumber)
                .IsUnicode(false);

            modelBuilder.Entity<judge>()
                .Property(e => e.telephone)
                .IsUnicode(false);

            modelBuilder.Entity<judge>()
                .Property(e => e.judgeID)
                .IsUnicode(false);

            modelBuilder.Entity<matchgroup>()
                .Property(e => e.gounpID)
                .IsUnicode(false);

            modelBuilder.Entity<matchgroup>()
                .Property(e => e.MajorJudge)
                .IsUnicode(false);

            modelBuilder.Entity<matchgroup>()
                .Property(e => e.judgeOne)
                .IsUnicode(false);

            modelBuilder.Entity<matchgroup>()
                .Property(e => e.judgeTwo)
                .IsUnicode(false);

            modelBuilder.Entity<matchgroup>()
                .Property(e => e.judgeThree)
                .IsUnicode(false);

            modelBuilder.Entity<matchgroup>()
                .Property(e => e.judgeFour)
                .IsUnicode(false);

            modelBuilder.Entity<matchgroup>()
                .Property(e => e.judgeFive)
                .IsUnicode(false);

            modelBuilder.Entity<teamlogin>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<teamlogin>()
                .Property(e => e.account)
                .IsUnicode(false);

            modelBuilder.Entity<teamlogin>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<adminlogin>()
                .Property(e => e.Admin)
                .IsUnicode(false);

            modelBuilder.Entity<adminlogin>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<coach>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<coach>()
                .Property(e => e.idNumber)
                .IsUnicode(false);

            modelBuilder.Entity<coach>()
                .Property(e => e.telephone)
                .IsUnicode(false);

            modelBuilder.Entity<coach>()
                .Property(e => e.gender)
                .IsUnicode(false);

            modelBuilder.Entity<judgelogin>()
                .Property(e => e.judgeName)
                .IsUnicode(false);

            modelBuilder.Entity<judgelogin>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<leader>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<leader>()
                .Property(e => e.idNumber)
                .IsUnicode(false);

            modelBuilder.Entity<leader>()
                .Property(e => e.telephone)
                .IsUnicode(false);

            modelBuilder.Entity<peasonalresult>()
                .Property(e => e.playID)
                .IsUnicode(false);

            modelBuilder.Entity<peasonalresult>()
                .Property(e => e.SportsEvent)
                .IsUnicode(false);

            modelBuilder.Entity<peasonalresult>()
                .Property(e => e.Gounpid)
                .IsUnicode(false);

            modelBuilder.Entity<teamresult>()
                .Property(e => e.teamName)
                .IsUnicode(false);

            modelBuilder.Entity<teamresult>()
                .Property(e => e.Event)
                .IsUnicode(false);
        }
    }
}
