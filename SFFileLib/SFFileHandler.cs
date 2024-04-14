using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SFFileLib
{
    public static class SFFileHandler
    {
        //List Directory
        public static List<Record> ListDirectory(AccountInfo accountInfo, string id, string path)
        {
            //Check if logged in
            if (accountInfo.UserID == null)
            {
                throw new Exception("Not logged in");
            }
        }
        //Download File
        //Upload File
        //Move File
        //Edit File Properties

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
