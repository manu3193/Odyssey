using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OdysseyCloud.DAL;
using OdysseyCloud.Models;

namespace OdysseyCloud.Controllers
{
    /// <summary>
    /// Controlador de autenticaci'on de usuario
    /// </summary>
    public class UserAuthController : ApiController
    {

        // GET: api/AuthUser
        /// <summary>
        /// Realiza la autentificacion de usuario, recibe un objeto que contiene la informacion de usuario
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>Retorna el identificador de usuario en caso de autenticarse correctamente, de otra forma retorna null</returns>
        public Guid? Get(UserInfo userInfo)
        {
            UserDB userDb = new UserDB();
            Guid? response;
            
            {
                response = userDb.AuthUserLogin(userInfo.Nickname, userInfo.Password);
            }
            return response;
        }
    
    }
}
