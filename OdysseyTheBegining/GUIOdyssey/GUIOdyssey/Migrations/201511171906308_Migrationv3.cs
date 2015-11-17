namespace GUIOdyssey.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrationv3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Albums", "ReleaseYear", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Albums", "ReleaseYear", c => c.String());
        }
    }
}
