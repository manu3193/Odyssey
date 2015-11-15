using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace GUIOdyssey.LogicLayer
{
    public class SessionManager
    {
        public Guid UserId { get; set; }

        public string Nickname { get; set; }

        public string Name { get; set; }

        private static SessionManager instance;

        private SessionManager() { }

        public static SessionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SessionManager();
                }
                return instance;
            }
        }
    }
}
