﻿using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SFFileLib
{
    public static class SFFileHandler
    {

        public static HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("https://api.resonite.com")
        };

        //List Directory
        public static async Task<List<Record>> ListDirectory(AccountInfo accountInfo, string inventoryId, string path)
        {
            //Check if logged in
            if (accountInfo.UserID == null)
            {
                throw new Exception("Not logged in");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new(accountInfo.FullToken!);
            var dirList = await _httpClient.GetFromJsonAsync<List<Record>>($"{getIdType(inventoryId)}/{inventoryId}/records?path={Uri.EscapeDataString(path)}");
            if (dirList == null)
            {
                throw new Exception("Directory List result is null.");
            }
            return dirList!;
            
        }
        //Download File
        public static async Task DownloadFile(AccountInfo accountInfo, Record record)
        {

        }
        //Upload File
        public static async Task UploadFile(AccountInfo accountInfo, string inventoryId, string pathFrom, string pathTo)
        {
            //Create Record object
            Record record = new()
            {
                RecordId = $"R-{Guid.NewGuid()}",
                RecordType = "object",
                OwnerId = inventoryId,
                Path = pathTo,
                AssetManifest = 
            };
            //Preprocess Record (Produces AssetDiff which contains assets I need to upload)
            //Upload Assets
        }
        //Move File
        //Edit File Properties
        //Delete File


        private static string getIdType(string id)
        {
            if (id.StartsWith('U'))
            {
                return "users";
            }
            else if (id.StartsWith('G'))
            {
                return "groups";
            }
            else
            {
                return "";
            }
        }
    }

    public class AccountInfo
    {
        public string Username;
        public string Password;
        public string? UserID;
        public string? Token;
        public string? FullToken;
        public Group[]? Groups;

        public static HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("https://api.resonite.com")
        };

        public AccountInfo(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public async static Task<AccountInfo> Login(AccountInfo accountInfo, string? totp = null)
        {
            LoginData loginData = new()
            {
                Username = accountInfo.Username,
                Authentication = new PassAuth { Password = accountInfo.Password },
                RememberMe = false,
                SecretMachineId = getMachineID()
            };
            
            _httpClient.DefaultRequestHeaders.Add("UID", getUID());
            AccountInfo loginDetails = new AccountInfo(accountInfo.Username, accountInfo.Password);
            var loginResponse = await _httpClient.PostAsJsonAsync("userSessions", loginData);

            if (loginResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var response = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();
                loginDetails.UserID = response!.Details.UserId;
                loginDetails.Token = response!.Details.SessionToken;
                loginDetails.FullToken = string.Format("res {0}:{1}", loginDetails.UserID, loginDetails.Token);
                _httpClient.DefaultRequestHeaders.Authorization = new(loginDetails.FullToken);
            }
            return loginDetails;
        }

        public async static Task<AccountInfo> GetGroups(AccountInfo accountInfo)
        {
            //Check if logged in
            if (accountInfo.UserID == null)
            {
                throw new Exception("Not logged in");
            }
            
            var groupDetailsResponse = await _httpClient.GetFromJsonAsync<Group[]>($"users/{accountInfo.UserID}/memberships");
            AccountInfo accountWithGroups = new(accountInfo.Username, accountInfo.Password)
            {
                UserID = accountInfo.UserID,
                Token = accountInfo.Token,
                FullToken = accountInfo.FullToken,
                Groups = groupDetailsResponse
            };
            return accountWithGroups;
        }

        private static string getMachineID()
        {
            Random random = new();
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_";
            string result = "";

            for (int i = 0; i < 128; i++)
            {
                result += characters[random.Next(characters.Length)].ToString();
            }

            return result;
        }

        private static string getUID()
        {
            Random random = new();
            return Convert.ToHexString(SHA256.HashData(RandomNumberGenerator.GetBytes(16)));
        }


    }

    
}
