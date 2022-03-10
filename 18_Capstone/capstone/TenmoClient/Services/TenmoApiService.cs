using RestSharp;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl;

        public TenmoApiService(string apiUrl) : base(apiUrl) { ApiUrl = apiUrl; }

        // Add methods to call api here...
        public Account GetAccount(int userId)
        {
            string url = $"{ApiUrl}account/{userId}";
            RestRequest request = new RestRequest(url);
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

        public List<Transfer> GetOwnTransfers(int userId)
        {
            string url = $"{ApiUrl}transfer/mytransfers/{userId}";
            RestRequest request = new RestRequest(url);
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);

            return response.Data;
        }

        public Transfer AddTransfer(Transfer transferToAdd)
        {
            string url = $"{ApiUrl}transfer/add";
            RestRequest request = new RestRequest(url);
            request.AddJsonBody(transferToAdd);
            IRestResponse<Transfer> response = client.Post<Transfer>(request);

            return response.Data;
        }
    }
}
