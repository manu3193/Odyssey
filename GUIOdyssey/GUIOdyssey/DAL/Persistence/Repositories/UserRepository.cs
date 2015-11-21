using System;
using System.Collections.Generic;
using System.Linq;
using GUIOdyssey.DAL.Persistence.Models;

namespace GUIOdyssey.DAL.Persistence.Repositories
{
    public class UserRepository : Repository<User>
    {
        public User GetUserById(Guid id)
        {
            return DbSet.Find(id);
        }

        public List<User> GetUsersByNickname(string nickname)
        {
            return DbSet.Where(u => u.Nickname.Equals(nickname)).ToList();
        }
    }
}
