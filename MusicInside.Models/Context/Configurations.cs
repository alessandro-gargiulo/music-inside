﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicInside.Models.AssociationClasses;
using MusicInside.Models.Models;

namespace MusicInside.Models.Context
{
    #region Single Class Configurations
    public class SongConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            #region Property Configurations
            builder.Property(p => p.Title)
                .IsRequired();

            builder.Property(p => p.TrackNo)
                .IsRequired()
                .HasDefaultValue(1);
            #endregion

            #region One-To-One Navigation Configurations
            builder.HasOne(p => p.Statistic)
                .WithOne(p => p.Song)
                .HasForeignKey<Song>(p => p.StatisticId)
                .IsRequired();

            builder.HasOne(p => p.Media)
                .WithOne(p => p.Song)
                .HasForeignKey<Song>(p => p.MediaId)
                .IsRequired();

            builder.HasOne(p => p.Media)
                .WithOne(p => p.Song)
                .HasForeignKey<Song>(p => p.MediaId)
                .IsRequired();
            #endregion

            #region One-To-Many Navigation Configurations
            builder.HasOne(p => p.Album)
                .WithMany(p => p.Songs)
                .HasForeignKey(p => p.AlbumId)
                .IsRequired();
            #endregion
        }
    }

    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            #region Property Configurations
            builder.Property(p => p.Title)
                .IsRequired();
            #endregion

            #region One-To-One Navigation Configurations
            builder.HasOne(p => p.Cover)
                .WithOne(p => p.Album)
                .HasForeignKey<Album>(p => p.CoverId)
                .IsRequired();
            #endregion

            #region One-To-Many Navigation Configurations
            builder.HasOne(p => p.Cover)
                .WithOne(p => p.Album)
                .HasForeignKey<Album>(p => p.CoverId)
                .IsRequired();
            #endregion
        }
    }

    public class CoverFileConfiguration : IEntityTypeConfiguration<CoverFile>
    {
        public void Configure(EntityTypeBuilder<CoverFile> builder)
        {
            #region Property Configurations
            builder.Property(p => p.Path).IsRequired();
            builder.Property(p => p.FileName).IsRequired();
            builder.Property(p => p.Extension).IsRequired();
            #endregion
        }
    }

    public class MediaFileConfiguration : IEntityTypeConfiguration<MediaFile>
    {
        public void Configure(EntityTypeBuilder<MediaFile> builder)
        {
            #region Property Configurations
            builder.Property(p => p.Path).IsRequired();
            builder.Property(p => p.FileName).IsRequired();
            builder.Property(p => p.Extension).IsRequired();
            #endregion
        }
    }
    #endregion

    #region Association Class Configurations
    public class SongGenreConfiguration : IEntityTypeConfiguration<SongGenre>
    {
        public void Configure(EntityTypeBuilder<SongGenre> builder)
        {
            #region Property Configurations
            builder.HasKey(k => new { k.SongId, k.GenreId });
            #endregion

            #region One-To-Many Navigation Configurations
            builder.HasOne(p => p.Genre)
                .WithMany(p => p.Songs)
                .HasForeignKey(p => p.GenreId)
                .IsRequired();

            builder.HasOne(p => p.Song)
                .WithMany(p => p.Genres)
                .HasForeignKey(p => p.SongId)
                .IsRequired();
            #endregion
        }
    }

    public class SongArtistConfiguration : IEntityTypeConfiguration<SongArtist>
    {
        public void Configure(EntityTypeBuilder<SongArtist> builder)
        {
            #region Property Configurations
            builder.HasKey(k => new { k.SongId, k.ArtistId });
            builder.Property(p => p.IsPrincipalArtist).IsRequired();
            #endregion

            #region One-To-Many Navigation Configurations
            builder.HasOne(p => p.Artist)
                .WithMany(p => p.Songs)
                .HasForeignKey(p => p.ArtistId)
                .IsRequired();

            builder.HasOne(p => p.Song)
                .WithMany(p => p.Artists)
                .HasForeignKey(p => p.SongId)
                .IsRequired();
            #endregion
        }
    }

    public class SongMomentConfiguration : IEntityTypeConfiguration<SongMoment>
    {
        public void Configure(EntityTypeBuilder<SongMoment> builder)
        {
            #region Property Configurations
            builder.HasKey(k => new { k.SongId, k.MomentId });
            #endregion

            #region One-To-Many Navigation Configurations
            builder.HasOne(p => p.Moment)
                .WithMany(p => p.Songs)
                .HasForeignKey(p => p.MomentId)
                .IsRequired();

            builder.HasOne(p => p.Song)
                .WithMany(p => p.Moments)
                .HasForeignKey(p => p.SongId)
                .IsRequired();
            #endregion
        }
    }
    #endregion
}
