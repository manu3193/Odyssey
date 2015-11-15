using System.Data.Entity;
using GUIOdyssey.DAL.Persistence.Models;


namespace GUIOdyssey.DAL.Persistence
{
    public class OdysseyContext : DbContext
    {

        public OdysseyContext() : base("name=OdysseyContext")
        {
            Database.SetInitializer<OdysseyContext>(new OdysseyContextInitializer());
        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserTrack> UserTracks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Album>()
                .HasMany(e => e.Tracks)
                .WithRequired(e => e.Album)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Artist>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Track>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Track>()
                .Property(e => e.Lyrics)
                .IsUnicode(false);

            modelBuilder.Entity<Track>()
                .Property(e => e.Path)
                .IsUnicode(false);

            modelBuilder.Entity<Track>()
                .Property(e => e.Genre)
                .IsUnicode(false);

            modelBuilder.Entity<UserTrack>()
                .HasKey(e => new { e.UserID, e.TrackID});

            modelBuilder.Entity<User>()
                .Property(e => e.Nickname)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);

        }
    }
}
