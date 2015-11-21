using System.Data.Entity.Migrations;
using GUIOdyssey.DAL.Persistence;

namespace GUIOdyssey.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<OdysseyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OdysseyContext context)
        {
        }
    }
}
