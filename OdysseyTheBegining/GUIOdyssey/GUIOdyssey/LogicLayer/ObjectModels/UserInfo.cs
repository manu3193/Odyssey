using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GUIOdyssey.LogicLayer.ObjectModels
{
    public class UserInfo
    {
        public Guid UserId { get; set; }
        public string Nickname { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}