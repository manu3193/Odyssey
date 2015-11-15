namespace GUIOdyssey.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        AlbumID = c.Guid(nullable: false),
                        ArtistID = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50, unicode: false),
                        MusicBrainzID = c.Guid(),
                        ReleaseYear = c.String(),
                    })
                .PrimaryKey(t => t.AlbumID)
                .ForeignKey("dbo.Artists", t => t.ArtistID, cascadeDelete: true)
                .Index(t => t.ArtistID);
            
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        ArtistID = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50, unicode: false),
                        MusicBrainzID = c.Guid(),
                    })
                .PrimaryKey(t => t.ArtistID);
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        TrackID = c.Guid(nullable: false),
                        AlbumID = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50, unicode: false),
                        Lyrics = c.String(unicode: false, storeType: "text"),
                        Path = c.String(nullable: false, maxLength: 100, unicode: false),
                        Genre = c.String(nullable: false, maxLength: 25, unicode: false),
                    })
                .PrimaryKey(t => t.TrackID)
                .ForeignKey("dbo.Albums", t => t.AlbumID)
                .Index(t => t.AlbumID);
            
            CreateTable(
                "dbo.UserTracks",
                c => new
                    {
                        UserID = c.Guid(nullable: false),
                        TrackID = c.Guid(nullable: false),
                        IsSync = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.TrackID })
                .ForeignKey("dbo.Tracks", t => t.TrackID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.TrackID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Guid(nullable: false),
                        Nickname = c.String(nullable: false, maxLength: 50, unicode: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTracks", "UserID", "dbo.Users");
            DropForeignKey("dbo.Tracks", "AlbumID", "dbo.Albums");
            DropForeignKey("dbo.UserTracks", "TrackID", "dbo.Tracks");
            DropForeignKey("dbo.Albums", "ArtistID", "dbo.Artists");
            DropIndex("dbo.UserTracks", new[] { "TrackID" });
            DropIndex("dbo.UserTracks", new[] { "UserID" });
            DropIndex("dbo.Tracks", new[] { "AlbumID" });
            DropIndex("dbo.Albums", new[] { "ArtistID" });
            DropTable("dbo.Users");
            DropTable("dbo.UserTracks");
            DropTable("dbo.Tracks");
            DropTable("dbo.Artists");
            DropTable("dbo.Albums");
        }
    }
}
