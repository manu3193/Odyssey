namespace GUIOdyssey.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationV2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Albums", "ReleaseYear", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Albums", "ReleaseYear", c => c.Int());
        }
    }
}
