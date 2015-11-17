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

        public UserTrack GetUserTrackByPK(Guid trackId,Guid userId)
        {
            Guid[] key = {userId, trackId};
            return  DbSet.Find(key);
        }
    }
}
