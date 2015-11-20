using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUIOdyssey.DAL.Persistence.Models;

namespace GUIOdyssey.DAL.Persistence.Repositories
{
    public class UserTrackRepository : Repository<UserTrack>
    {

        public UserTrack GetUserTrackByPK(Guid userId,Guid trackId)
        {
            return  DbSet.Find(userId,trackId);
        }
    }
}
