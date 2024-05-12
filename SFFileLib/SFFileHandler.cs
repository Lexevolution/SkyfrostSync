using ImageMagick;
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
            //Resonite's upload flow:
            // - Start: SkyFrost.Base.RecordUploadTaskBase`1.RunUploadInternal
            // - SkyFrost.Base.RecordUploadTaskBase`1.CheckCloudVersion (Mainly used in worlds, likely don't need to worry about this, and can set both Local and Global version to 0)
            // - FrooxEngine.EngineRecordUploadTask.PrepareRecord
            // -- FrooxEngine.EngineRecordUploadTask.BuildManifest (Struggling to figure out what this does, I think it's just a string list of all the asset uris in the datatree?)
            // -- FrooxEngine.EngineRecordUploadTask.CollectAssets (Generates cloud db url and adds to AssetManifest)
            // --- FrooxEngine.Store.LocalDB.TryFetchAssetRecordWithMetadataAsync
            // ---- SkyFrost.Base.AssetUtil.GenerateHashSignature (Here it is!!! SHA256 hash of the file, that's it.) (It saves this and where the local file is stored in an "AssetUploadData" object list. It gets used eventually, but I'm missing some steps.)
            // - SkyFrost.Base.RecordUploadTaskBase`1.RemoveManifestDuplicates
            // - FrooxEngine.EngineRecordUploadTask.PreprocessRecord



            if (accountInfo.UserID == null)
            {
                throw new Exception("Not logged in");
            }

            //Create Record object
            Record record = new()
            {
                RecordId = $"R-{Guid.NewGuid()}",
                RecordType = "object",
                OwnerId = inventoryId,
                Path = pathTo,
                AssetManifest = new List<DBAsset>(),
                CreationTime = DateTime.Now,
                LastModificationTime = DateTime.Now,
                Version = new RecordVersion{ GlobalVersion = 0, LocalVersion = 1 }          
            };

            //Prepare asset(s)
            MagickNET.Initialize();
            var image = new MagickImage(MagickColors.Transparent, 512, 512);
            var iconSettings = new MagickReadSettings { Width = 448, Height = 448 };
            var icon = new MagickImage("E:\\Downloads\\draft_24dp_FILL1_wght300_GRAD0_opsz20.svg", iconSettings);
            var textSettings = new MagickReadSettings { 
                Font = "@E:\\Downloads\\Poppins-Medium.ttf",
                TextGravity = Gravity.Center,
                FillColor = MagickColors.White,
                StrokeColor = MagickColors.Black,
                Width = 496,
                Height = 64,
                FontPointsize = 8
            };

            var iconText = new MagickImage($"caption: {Path.GetFileName(pathFrom)}", textSettings);

            image.Composite(icon, Gravity.North);
            image.Composite(iconText, Gravity.South);
            image.Write("E:\\Downloads\\filevisual.webp");
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
                _httpClient.DefaultRequestHeaders.Add("Authorization", loginDetails.FullToken);
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
