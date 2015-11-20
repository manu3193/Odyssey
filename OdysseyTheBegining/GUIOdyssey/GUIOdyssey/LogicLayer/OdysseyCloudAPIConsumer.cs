using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUIOdyssey.LogicLayer.ObjectModels;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GUIOdyssey.LogicLayer
{
    public class OdysseyCloudAPIConsumer
    {
        /// <summary>
        /// Metodo encargado de consumir el Web API para obtener la autenticaci'on de usuario
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>Retorna la informaci'on de usuario en caso de existir, sino retorna un UserInfo con propiedades nulas o bien
        /// un null en caso de no tener exito</returns>
        public async Task<UserInfo> GetUserAuth(UserInfo userInfo)
        {
            UserInfo userResponse =null;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:1444/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            

            HttpResponseMessage response = await client.PostAsJsonAsync("api/AuthUser", userInfo);

            if (response.IsSuccessStatusCode)
            {
                userResponse = response.Content.ReadAsAsync<UserInfo>().Result;
            }
            return userResponse;
        }

        public async Task<UserInfo> CreateUser(UserInfo userInfo)
        {
            UserInfo userResponse = null;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:1444/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            HttpResponseMessage response = await client.PostAsJsonAsync("api/NewUser", userInfo);

            if (response.IsSuccessStatusCode)
            {
                userResponse = response.Content.ReadAsAsync<UserInfo>().Result;
            }
            return userResponse;
        }

        /// <summary>
        /// Metodo encargado de consumer wl Web API para insertar una nueva metadata de canci'on
        /// </summary>
        /// <param name="trackInfo">Informaci'on de la canci'on</param>
        /// <returns></returns>
        public async Task InsertTrackMetadata(TrackInfo trackInfo)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:1444/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsJsonAsync("api/InsertTrack", trackInfo);

            if (response.IsSuccessStatusCode)
            {
                //success
            }
        }

        /// <summary>
        /// Metodo que consume la REST API para obtener la lista de canciones en el cloud de un usuario determinado
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<TrackInfo>> GetUserTrackColletion(Guid userId)
        {
            List < TrackInfo> trackInfoList = null;
            HttpClient client =new HttpClient();
            client.BaseAddress = new Uri("http://localhost:1444/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // HTTP GET
            string userIdString = userId.ToString();
            string route = "api/UserTracks/" + userIdString;
            HttpResponseMessage response = await client.GetAsync(route);
            if (response.IsSuccessStatusCode)
            {
                trackInfoList = await response.Content.ReadAsAsync<List<TrackInfo>>();
            }
            return trackInfoList;
        }
    }
}
