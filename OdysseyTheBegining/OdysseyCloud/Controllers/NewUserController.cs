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
    /// Controlador para ingreso de nuevos usuarios
    /// </summary>
    public class NewUserController : ApiController
    {
        /// <summary>
        /// Se utiliza para crear nuevos usuarios en Odyssey
        /// </summary>
        /// <param name="userInfo">Objeto que describe los atributos de un usuario</param>
        /// <returns>Retorna el Id de usuario en caso de crearlo con exito, de otra forma retorna null</returns>
        public Guid? Post(UserInfo userInfo)
        {
            UserDB userDb = new UserDB();
            Guid? response;

            {
                response = userDb.CreateNewUser(userInfo.Nickname,userInfo.Name, userInfo.Password);
            }
            return response;
        }
    }
}

