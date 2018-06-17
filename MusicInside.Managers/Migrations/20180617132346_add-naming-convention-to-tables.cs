using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicInside.Models.Migrations
{
    public partial class addnamingconventiontotables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_Statistics_StatisticId",
                table: "Song");

            migrationBuilder.DropForeignKey(
                name: "FK_SongArtist_Artists_ArtistId",
                table: "SongArtist");

            migrationBuilder.DropForeignKey(
                name: "FK_SongGenre_Genres_GenreId",
                table: "SongGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_SongMoment_Moments_MomentId",
                table: "SongMoment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statistics",
                table: "Statistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Moments",
                table: "Moments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Artists",
                table: "Artists");

            migrationBuilder.RenameTable(
                name: "Statistics",
                newName: "Statistic");

            migrationBuilder.RenameTable(
                name: "Moments",
                newName: "Moment");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Genre");

            migrationBuilder.RenameTable(
                name: "Artists",
                newName: "Artist");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statistic",
                table: "Statistic",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Moment",
                table: "Moment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genre",
                table: "Genre",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Artist",
                table: "Artist",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Statistic_StatisticId",
                table: "Song",
                column: "StatisticId",
                principalTable: "Statistic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SongArtist_Artist_ArtistId",
                table: "SongArtist",
                column: "ArtistId",
                principalTable: "Artist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SongGenre_Genre_GenreId",
                table: "SongGenre",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SongMoment_Moment_MomentId",
                table: "SongMoment",
                column: "MomentId",
                principalTable: "Moment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_Statistic_StatisticId",
                table: "Song");

            migrationBuilder.DropForeignKey(
                name: "FK_SongArtist_Artist_ArtistId",
                table: "SongArtist");

            migrationBuilder.DropForeignKey(
                name: "FK_SongGenre_Genre_GenreId",
                table: "SongGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_SongMoment_Moment_MomentId",
                table: "SongMoment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statistic",
                table: "Statistic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Moment",
                table: "Moment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genre",
                table: "Genre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Artist",
                table: "Artist");

            migrationBuilder.RenameTable(
                name: "Statistic",
                newName: "Statistics");

            migrationBuilder.RenameTable(
                name: "Moment",
                newName: "Moments");

            migrationBuilder.RenameTable(
                name: "Genre",
                newName: "Genres");

            migrationBuilder.RenameTable(
                name: "Artist",
                newName: "Artists");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statistics",
                table: "Statistics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Moments",
                table: "Moments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Artists",
                table: "Artists",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Statistics_StatisticId",
                table: "Song",
                column: "StatisticId",
                principalTable: "Statistics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SongArtist_Artists_ArtistId",
                table: "SongArtist",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SongGenre_Genres_GenreId",
                table: "SongGenre",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SongMoment_Moments_MomentId",
                table: "SongMoment",
                column: "MomentId",
                principalTable: "Moments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
