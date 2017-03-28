using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MusicInside.Models
{
    public class SongDBContext : DbContext
    {
        public SongDBContext(DbContextOptions<SongDBContext> options) : base(options) { }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Moment> Moments { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Featuring> Featurings { get; set; }
        public DbSet<SongGenre> SongGenres { get; set; }
        public DbSet<SongMoment> SongMoments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
             * 
             * Required properties section
             * 
             */

            // Song table
            modelBuilder.Entity<Song>()
                .Property(p => p.Title)
                .IsRequired();
            // File table
            modelBuilder.Entity<File>()
                .Property(p => p.Path)
                .IsRequired();
            modelBuilder.Entity<File>()
                .Property(p => p.FileName)
                .IsRequired();
            modelBuilder.Entity<File>()
                .Property(p => p.Extension)
                .IsRequired();
            // Album table
            modelBuilder.Entity<Album>()
                .Property(p => p.Title)
                .IsRequired();
            // Artist table
            modelBuilder.Entity<Artist>()
                .Property(p => p.ArtName)
                .IsRequired();
            // Genre table
            modelBuilder.Entity<Genre>()
                .Property(p => p.Description)
                .IsRequired();
            // Moment table
            modelBuilder.Entity<Moment>()
                .Property(p => p.Description)
                .IsRequired();

            /*
             * 
             * Non-standard key definition section
             * 
             */

            // Many-to-Many [Song-Genre] table ID definition
            modelBuilder.Entity<SongGenre>()
                .HasKey(t => new { t.SongId, t.GenreId });

            // Many-to-Many [Song-Moment] table ID definition
            modelBuilder.Entity<SongMoment>()
                .HasKey(t => new { t.SongId, t.MomentId });

            // Many-to-Many Associative [Featuring] table ID definition
            modelBuilder.Entity<Featuring>()
                .HasKey(t => new { t.SongId, t.ArtistId });

            /*
             * 
             * Navigation rules definition
             * 
             */

            // One-to-One relationship between Song and File tables
            modelBuilder.Entity<Song>()
                .HasOne(a => a.File)
                .WithOne(b => b.Song)
                .HasForeignKey<File>(c => c.SongId);

            // One-to-One relationship between Song and Statistic tables
            modelBuilder.Entity<Song>()
                .HasOne(a => a.Statistic)
                .WithOne(b => b.Song)
                .HasForeignKey<Statistic>(c => c.SongId);

            // One-to-One relationship between Album and File tables
            modelBuilder.Entity<File>()
                .HasOne(a => a.Album)
                .WithOne(b => b.File)
                .HasForeignKey<Album>(c => c.FileId);

            // One-to-Many relationship between Song and Album tables
            modelBuilder.Entity<Song>()
                .HasOne(a => a.Album)
                .WithMany(b => b.Songs)
                .HasForeignKey(c => c.AlbumId)
                .OnDelete(DeleteBehavior.SetNull);

            // Mapping entities to real tables
            modelBuilder.Entity<Song>().ToTable("Song");
            modelBuilder.Entity<Genre>().ToTable("Genre");
            modelBuilder.Entity<Artist>().ToTable("Artist");
            modelBuilder.Entity<Album>().ToTable("Album");
            modelBuilder.Entity<File>().ToTable("File");
            modelBuilder.Entity<Moment>().ToTable("Moment");
            modelBuilder.Entity<Statistic>().ToTable("Statistic");
            modelBuilder.Entity<Featuring>().ToTable("Featuring");
            modelBuilder.Entity<SongGenre>().ToTable("SongGenre");
            modelBuilder.Entity<SongMoment>().ToTable("SongMoment");
        }
    }
}