using RestSharp;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl;

        public TenmoApiService(string apiUrl) : base(apiUrl) { }

        // Add methods to call api here...
        public Account GetAccount(int userId)
        {
<<<<<<< HEAD
            
            RestRequest request = new RestRequest($"account/{userId}");
=======
            string url = $"{ApiUrl}account/{userId}";
            RestRequest request = new RestRequest(url);
>>>>>>> be1d598846c6d7349d0f18f6603929874064516c
            IRestResponse<Account> response = client.Get<Account>(request);

            //CheckForError(response);

            return response.Data;
        }

        public List<ApiUser> GetUsers()
        {
            string url = $"{ApiUrl}login";
            RestRequest request = new RestRequest(url);
            IRestResponse<List<ApiUser>> response = client.Get<List<ApiUser>>(request);

            //CheckForError(response);

            return response.Data;
        }
    }
}
