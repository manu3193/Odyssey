using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUIOdyssey.DAL.Persistence.Models;

namespace GUIOdyssey.DAL.Persistence.Repositories
{
    class AlbumRepository : Repository<Album>
    {
        public Album GetAlbumById(Guid albumId)
        {
            return DbSet.Find(albumId);
        }
        public List<Album> GetAlbumsByArtistId(Guid artistId)
        {
            return DbSet.Where(a => a.Artist.ArtistID.Equals(artistId)).ToList();
        }

        public List<Album> GetAlbumsByName(string title)
        {
            return DbSet.Where(a => a.Title.Equals(title)).ToList();
        }
    }
}
