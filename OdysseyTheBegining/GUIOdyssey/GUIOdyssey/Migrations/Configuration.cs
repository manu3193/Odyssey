using GUIOdyssey.DAL.Persistence.Models;

namespace GUIOdyssey.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GUIOdyssey.DAL.Persistence.OdysseyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GUIOdyssey.DAL.Persistence.OdysseyContext context)
        {
        }
    }
}
