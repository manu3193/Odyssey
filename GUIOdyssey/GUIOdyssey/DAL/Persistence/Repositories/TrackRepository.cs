using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUIOdyssey.DAL.Persistence.Models;

namespace GUIOdyssey.DAL.Persistence.Repositories
{
    class TrackRepository : Repository<Track>
    {

        public Track GetTrackById(Guid trackId)
        {
            return DbSet.Find(trackId);
        }

        public List<Track> GetTracksByAlbumId(Guid albumId)
        {
            return DbSet.Where(t => t.Album.AlbumID.Equals(albumId)).ToList();
        }

        public List<Track> GetTraksByUserId(Guid userId)
        {
            return DbSet.Where(t => t.UserTracks.Any(u => u.UserID.Equals(userId))).ToList();
        } 
    }
}
