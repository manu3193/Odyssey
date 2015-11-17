using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OdysseyCloud.DAL
{
    public class UserDB 
    {
        /// <summary>
        /// Metodo encargado de ejecutar el procedimiento almacenado de la base de datos
        /// para autentificar un usuari
        /// </summary>
        /// <param name="nickname">Nickname de usuario</param>
        /// <param name="password">Contrase;a de usuario</param>
        /// <returns>EL id de usuario en caso de identificarse correctamente, de otra forma devuelve null</returns>
        public  Guid? AuthUserLogin(string nickname, string password)
        {
            Guid? userId;
            using (SqlConnection connection = new SqlConnection(DBConfigurator.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_AuthUserLogin", connection))
            {

                
                SqlParameter outputParam = cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier);
                outputParam.Direction = ParameterDirection.Output;
                SqlParameter nicknameParameter = cmd.Parameters.Add("@Nickname", SqlDbType.NVarChar);
                nicknameParameter.Direction = ParameterDirection.Input;
                nicknameParameter.Value = nickname;
                SqlParameter passwordParameter = cmd.Parameters.Add("@Password", SqlDbType.NVarChar);
                passwordParameter.Direction = ParameterDirection.Input;
                passwordParameter.Value = password;

                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();
                cmd.ExecuteNonQuery();

                userId = outputParam.Value as Guid?;
                connection.Close();
            }
        return userId;
        }

        /// <summary>
        /// Metodo encargado de llamar el procedimiento almacenado de la base de datos para ingresar un nuevo usuario
        /// </summary>
        /// <param name="nickname">Nickname de usuario</param>
        /// <param name="name">Nombre del usuario</param>
        /// <param name="password">Contrase;a</param>
        /// <returns></returns>
        public Guid? CreateNewUser(string nickname, string name, string password)
        {
            Guid? userId;
            
            using (SqlConnection connection = new SqlConnection(DBConfigurator.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_newUser", connection))
            {
                SqlParameter outputParam = cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier);
                outputParam.Direction = ParameterDirection.Output;
                SqlParameter nicknameParameter = cmd.Parameters.Add("@Nickname", SqlDbType.NVarChar);
                nicknameParameter.Direction = ParameterDirection.Input;
                nicknameParameter.Value = nickname;
                SqlParameter passwordParameter = cmd.Parameters.Add("@Password", SqlDbType.NVarChar);
                passwordParameter.Direction = ParameterDirection.Input;
                passwordParameter.Value = password;
                SqlParameter nameParameter = cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                nameParameter.Direction = ParameterDirection.Input;
                nameParameter.Value = name;

                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();
                cmd.ExecuteNonQuery();

                userId = outputParam.Value as Guid?;
                connection.Close();
            }
            return userId;
        }
    }
}