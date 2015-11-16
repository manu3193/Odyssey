using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OdysseyCloud.DAL;

namespace OdysseyCloud.Controllers
{
    public class UserController : ApiController
    {

        // GET: api/User
        public Guid? Get()
        {
            UserDB userDb = new UserDB();
            Guid? response;
            
            {
                response = userDb.AuthUserLogin("manzumabado", "cacabubu");
            }
            return response;
        }
    
    }
}
