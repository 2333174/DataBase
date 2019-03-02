namespace DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<athlete> athlete { get; set; }
        public virtual DbSet<doctor> doctor { get; set; }
        public virtual DbSet<judge> judge { get; set; }
        public virtual DbSet<leader> leader { get; set; }
        public virtual DbSet<team> team { get; set; }
        public virtual DbSet<coach> coach { get; set; }
        public virtual DbSet<login> login { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<athlete>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<athlete>()
                .Property(e => e.idNumber)
                .IsUnicode(false);

            modelBuilder.Entity<athlete>()
                .Property(e => e.group)
                .IsUnicode(false);

            modelBuilder.Entity<athlete>()
                .Property(e => e.sportsEvent)
                .IsUnicode(false);

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

            modelBuilder.Entity<leader>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<leader>()
                .Property(e => e.idNumber)
                .IsUnicode(false);

            modelBuilder.Entity<leader>()
                .Property(e => e.telephone)
                .IsUnicode(false);

            modelBuilder.Entity<team>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<team>()
                .Property(e => e.account)
                .IsUnicode(false);

            modelBuilder.Entity<team>()
                .Property(e => e.password)
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
                .Property(e => e.sex)
                .IsUnicode(false);

            modelBuilder.Entity<login>()
                .Property(e => e.User)
                .IsUnicode(false);

            modelBuilder.Entity<login>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
