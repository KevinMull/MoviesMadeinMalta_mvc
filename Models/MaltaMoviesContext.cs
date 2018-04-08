using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MaltaMoviesMVCcore.Models
{
    public partial class MaltaMoviesContext : DbContext
    {
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<LocationAlias> LocationAliases { get; set; }
        public virtual DbSet<LocationPlace> LocationPlaces { get; set; }
        public virtual DbSet<LocationSite> LocationSites { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Scene> Scenes { get; set; }

        public MaltaMoviesContext(DbContextOptions<MaltaMoviesContext> options)
            : base(options)
        { }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Comment>(entity =>
        //    {
        //        entity.HasKey(e => e.CommentId);

        //        entity.Property(e => e.CommentId).HasColumnName("CommentID");

        //        entity.Property(e => e.Comments1)
        //            .HasColumnName("Comments")
        //            .HasColumnType("ntext");

        //        entity.Property(e => e.Ipaddress)
        //            .HasColumnName("IPAddress")
        //            .HasMaxLength(50);

        //        entity.Property(e => e.UserEmail)
        //            .HasColumnName("UserEMail")
        //            .HasMaxLength(50);

        //        entity.Property(e => e.UserName).HasMaxLength(50);
        //    });

        //    modelBuilder.Entity<LocationAliase>(entity =>
        //    {
        //        entity.HasKey(e => e.LocationAliasId);

        //        entity.Property(e => e.LatLong)
        //            .HasMaxLength(102)
        //            .IsUnicode(false)
        //            .HasComputedColumnSql("((ltrim(str([Latitude],(50),(6)))+', ')+ltrim(str([Longitude],(50),(6))))");

        //        entity.Property(e => e.LocationAliasCountry).HasMaxLength(255);

        //        entity.Property(e => e.LocationAliasPlace).HasMaxLength(255);
        //    });

        //    modelBuilder.Entity<LocationPlace>(entity =>
        //    {
        //        entity.HasKey(e => e.LocationPlaceId);

        //        entity.Property(e => e.LocationPlaceName).HasMaxLength(255);
        //    });

        //    modelBuilder.Entity<LocationSite>(entity =>
        //    {
        //        entity.HasKey(e => e.LocationSiteId);

        //        entity.Property(e => e.GoogleMapUrl)
        //            .HasColumnName("GoogleMapURL")
        //            .HasColumnType("ntext");

        //        entity.Property(e => e.LatLong)
        //            .HasMaxLength(102)
        //            .IsUnicode(false)
        //            .HasComputedColumnSql("((ltrim(str([Latitude],(50),(6)))+', ')+ltrim(str([Longitude],(50),(6))))");

        //        entity.Property(e => e.LocationHiResImagePhysicalPath)
        //            .HasMaxLength(130)
        //            .IsUnicode(false)
        //            .HasComputedColumnSql("(('C:\\Users\\Kevin\\Documents\\visual studio 2012\\Projects\\moviesmadeinmaltav2\\Images\\locations\\HiRes\\'+CONVERT([varchar],[LocationSiteId]))+'.jpg')");

        //        entity.Property(e => e.LocationLargeImagePhysicalPath)
        //            .HasMaxLength(124)
        //            .IsUnicode(false)
        //            .HasComputedColumnSql("(('C:\\Users\\Kevin\\Documents\\visual studio 2012\\Projects\\moviesmadeinmaltav2\\Images\\locations\\'+CONVERT([varchar],[LocationSiteId]))+'.jpg')");

        //        entity.Property(e => e.LocationSiteName).HasMaxLength(100);

        //        entity.HasOne(d => d.LocationPlace)
        //            .WithMany(p => p.LocationSites)
        //            .HasForeignKey(d => d.LocationPlaceId)
        //            .OnDelete(DeleteBehavior.Cascade)
        //            .HasConstraintName("FK_LocationSites_LocationPlaces");
        //    });

        //    modelBuilder.Entity<Movie>(entity =>
        //    {
        //        entity.HasKey(e => e.TitleId);

        //        entity.Property(e => e.CoverArtPhysicalPath)
        //            .HasMaxLength(121)
        //            .IsUnicode(false)
        //            .HasComputedColumnSql("(('C:\\Users\\Kevin\\Documents\\visual studio 2012\\Projects\\moviesmadeinmaltav2\\Images\\titles\\'+CONVERT([varchar],[TitleId]))+'.jpg')");

        //        entity.Property(e => e.CoverArtWebPath)
        //            .HasMaxLength(49)
        //            .IsUnicode(false)
        //            .HasComputedColumnSql("(('/images/titles/'+CONVERT([varchar],[TitleId]))+'.jpg')");

        //        entity.Property(e => e.ImdbUrl)
        //            .HasColumnName("ImdbURL")
        //            .HasMaxLength(500);

        //        entity.Property(e => e.Title).HasMaxLength(255);

        //        entity.Property(e => e.TitleAndYear)
        //            .HasMaxLength(288)
        //            .HasComputedColumnSql("((([Title]+' (')+CONVERT([varchar],[TitleYear]))+')')");
        //    });

        //    modelBuilder.Entity<Scene>(entity =>
        //    {
        //        entity.HasKey(e => e.SceneId);

        //        entity.Property(e => e.Notes).HasMaxLength(255);

        //        entity.Property(e => e.SceneLargeImagePhysicalPath)
        //            .HasMaxLength(127)
        //            .IsUnicode(false)
        //            .HasComputedColumnSql("(('C:\\Users\\Kevin\\Documents\\visual studio 2012\\Projects\\moviesmadeinmaltav2\\Images\\scenes\\large\\'+CONVERT([varchar],[SceneId]))+'.jpg')");

        //        entity.Property(e => e.SceneLargeImageWebPath)
        //            .HasMaxLength(55)
        //            .IsUnicode(false)
        //            .HasComputedColumnSql("(('/images/scenes/large/'+CONVERT([varchar],[SceneId]))+'.jpg')");

        //        entity.Property(e => e.SceneThumbImagePhysicalPath)
        //            .HasMaxLength(128)
        //            .IsUnicode(false)
        //            .HasComputedColumnSql("(('C:\\Users\\Kevin\\Documents\\visual studio 2012\\Projects\\moviesmadeinmaltav2\\Images\\scenes\\thumbs\\'+CONVERT([varchar],[SceneId]))+'.jpg')");

        //        entity.Property(e => e.SceneThumbImageWebPath)
        //            .HasMaxLength(56)
        //            .IsUnicode(false)
        //            .HasComputedColumnSql("(('/images/scenes/thumbs/'+CONVERT([varchar],[SceneId]))+'.jpg')");

        //        entity.HasOne(d => d.LocationAlias)
        //            .WithMany(p => p.Scenes)
        //            .HasForeignKey(d => d.LocationAliasId)
        //            .HasConstraintName("FK_Scenes_LocationAliases");

        //        entity.HasOne(d => d.LocationSite)
        //            .WithMany(p => p.Scenes)
        //            .HasForeignKey(d => d.LocationSiteId)
        //            .OnDelete(DeleteBehavior.Cascade)
        //            .HasConstraintName("FK_Scenes_LocationSites");

        //        entity.HasOne(d => d.Title)
        //            .WithMany(p => p.Scenes)
        //            .HasForeignKey(d => d.TitleId)
        //            .OnDelete(DeleteBehavior.Cascade)
        //            .HasConstraintName("FK_Scenes_Movies");
        //    });
    }

    
}
