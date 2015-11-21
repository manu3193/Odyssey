using GUIOdyssey.DAL.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIOdyssey.DAL.Persistence.Repositories
{
    class ArtistRepository : Repository<Artist>
    {


        public Artist GetArtistById(Guid Id)
        {
            return DbSet.Find(Id);
        }

        public List<Artist> GetArtistByTitle(string title)
        {
            return DbSet.Where(a => a.Title.Equals(title)).ToList();
        }
    }
}
