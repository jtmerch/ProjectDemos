using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TSYSWasm.Models;

namespace TSYSWasm.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient client,
                                     AuthenticationStateProvider authStateProvider,
                                     ILocalStorageService localStorage)
        {
            this._client = client;
            this._authStateProvider = authStateProvider;
            this._localStorage = localStorage;
        }


        //Login
        public async Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication)
        {

            //var data = new FormUrlEncodedContent(new[]
            //{
            //    new KeyValuePair<string, string>("grant_type", "password"),
            //      new KeyValuePair<string, string>("LoginID", userForAuthentication.Email),
            //        new KeyValuePair<string, string>("Password", userForAuthentication.Password)
            //});


            var jsonData = new
            {
                grant_type = "password",
                LoginID = userForAuthentication.Email,
                Password = userForAuthentication.Password
            };

            var jsonstring = JsonConvert.SerializeObject(jsonData);
            var stringContent = new StringContent(jsonstring, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+


            var authResult = await _client.PostAsync("http://localhost:5003/api/token", stringContent);
            var authContent = await authResult.Content.ReadAsStringAsync();

            if (authResult.IsSuccessStatusCode == false)
            {
                return null;
            }

            //if it is sucessful
            var result = System.Text.Json.JsonSerializer.Deserialize<AuthenticatedUserModel>(authContent);


            await _localStorage.SetItemAsync("authToken", result.token);

            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.token);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.token);

            return result;

        }

        //Logout method
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null; //wipeout the request headers
        }

    }
}
