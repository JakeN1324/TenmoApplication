﻿using RestSharp;
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
            string url = ApiUrl + "account/";
            RestRequest request = new RestRequest(url);
            IRestResponse<Account> response = client.Get<Account>(request);

            //CheckForError(response);

            return response.Data;
        }

    }
}
