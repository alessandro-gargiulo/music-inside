using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MusicInside.Models;

namespace MusicInside.Migrations
{
    [DbContext(typeof(SongDBContext))]
    [Migration("20170301232633_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MusicInside.Models.Album", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArtistId");

                    b.Property<int?>("FileId");

                    b.Property<bool>("IsSingle");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("ArtistId");

                    b.HasIndex("FileId")
                        .IsUnique();

                    b.ToTable("Album");
                });

            modelBuilder.Entity("MusicInside.Models.Artist", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArtName")
                        .IsRequired();

                    b.Property<DateTime>("BirthYear");

                    b.Property<string>("Name");

                    b.Property<string>("Surname");

                    b.HasKey("ID");

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("MusicInside.Models.Featuring", b =>
                {
                    b.Property<int>("SongId");

                    b.Property<int>("ArtistId");

                    b.Property<bool>("IsPrincipalArtist");

                    b.HasKey("SongId", "ArtistId");

                    b.HasIndex("ArtistId");

                    b.HasIndex("SongId");

                    b.ToTable("Featuring");
                });

            modelBuilder.Entity("MusicInside.Models.File", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AlbumId");

                    b.Property<string>("Extension")
                        .IsRequired();

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.Property<string>("Path")
                        .IsRequired();

                    b.Property<int?>("SongId");

                    b.HasKey("ID");

                    b.HasIndex("SongId")
                        .IsUnique();

                    b.ToTable("File");
                });

            modelBuilder.Entity("MusicInside.Models.Genre", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("MusicInside.Models.Moment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Moment");
                });

            modelBuilder.Entity("MusicInside.Models.Song", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AlbumID");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("TrackNo");

                    b.Property<int>("Year");

                    b.HasKey("ID");

                    b.HasIndex("AlbumID");

                    b.ToTable("Song");
                });

            modelBuilder.Entity("MusicInside.Models.SongGenre", b =>
                {
                    b.Property<int>("SongId");

                    b.Property<int>("GenreId");

                    b.HasKey("SongId", "GenreId");

                    b.HasIndex("GenreId");

                    b.HasIndex("SongId");

                    b.ToTable("SongGenre");
                });

            modelBuilder.Entity("MusicInside.Models.SongMoment", b =>
                {
                    b.Property<int>("SongId");

                    b.Property<int>("MomentId");

                    b.HasKey("SongId", "MomentId");

                    b.HasIndex("MomentId");

                    b.HasIndex("SongId");

                    b.ToTable("SongMoment");
                });

            modelBuilder.Entity("MusicInside.Models.Statistic", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastPlay");

                    b.Property<int>("NumPlay");

                    b.Property<int>("SongId");

                    b.HasKey("ID");

                    b.HasIndex("SongId")
                        .IsUnique();

                    b.ToTable("Statistic");
                });

            modelBuilder.Entity("MusicInside.Models.Album", b =>
                {
                    b.HasOne("MusicInside.Models.Artist", "Artist")
                        .WithMany("Albums")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MusicInside.Models.File", "File")
                        .WithOne("Album")
                        .HasForeignKey("MusicInside.Models.Album", "FileId");
                });

            modelBuilder.Entity("MusicInside.Models.Featuring", b =>
                {
                    b.HasOne("MusicInside.Models.Artist", "Artist")
                        .WithMany("Featurings")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MusicInside.Models.Song", "Song")
                        .WithMany("Featurings")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MusicInside.Models.File", b =>
                {
                    b.HasOne("MusicInside.Models.Song", "Song")
                        .WithOne("File")
                        .HasForeignKey("MusicInside.Models.File", "SongId");
                });

            modelBuilder.Entity("MusicInside.Models.Song", b =>
                {
                    b.HasOne("MusicInside.Models.Album", "Album")
                        .WithMany("Songs")
                        .HasForeignKey("AlbumID")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("MusicInside.Models.SongGenre", b =>
                {
                    b.HasOne("MusicInside.Models.Genre", "Genre")
                        .WithMany("SongGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MusicInside.Models.Song", "Song")
                        .WithMany("SongGenres")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MusicInside.Models.SongMoment", b =>
                {
                    b.HasOne("MusicInside.Models.Moment", "Moment")
                        .WithMany("SongMoments")
                        .HasForeignKey("MomentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MusicInside.Models.Song", "Song")
                        .WithMany("SongMoments")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MusicInside.Models.Statistic", b =>
                {
                    b.HasOne("MusicInside.Models.Song", "Song")
                        .WithOne("Statistic")
                        .HasForeignKey("MusicInside.Models.Statistic", "SongId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
