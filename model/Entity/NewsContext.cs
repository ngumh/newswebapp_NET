namespace model.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NewsContext : DbContext
    {
        public NewsContext()
            : base("name=NewsContext2")
        {
        }

        public virtual DbSet<Catagory> Catagory { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Subscriber> Subscriber { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catagory>()
                .HasMany(e => e.Post)
                .WithOptional(e => e.Catagory)
                .HasForeignKey(e => e.Catagory_Id);

            modelBuilder.Entity<Messages>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.ImageURL)
                .IsUnicode(false);

            modelBuilder.Entity<Subscriber>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PasswordHash)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Post)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Author_Id);
        }
    }
}
