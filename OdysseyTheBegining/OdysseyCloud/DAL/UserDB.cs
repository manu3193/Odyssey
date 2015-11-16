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
    }
}