using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SFFileLib
{
    internal class LoginData
    {
        [JsonPropertyName("username")]
        public required string Username { get; set; }

        [JsonPropertyName ("rememberMe")]
        public required bool RememberMe { get; set; }

        [JsonPropertyName("authentication")]
        public required Auth Authentication { get; set; }

        [JsonPropertyName("secretMachineId")]
        public required string SecretMachineId { get; set; }
    }

    internal class PassAuth : Auth
    {
        [JsonPropertyName("password")]
        public required string Password { get; set; }
    }

    [JsonDerivedType(typeof(PassAuth), "password")]
    internal abstract class Auth{ }


    public class LoginDetails
    {
        [JsonPropertyName("userId")]
        public required string UserId { get; set; }

        [JsonPropertyName("token")]
        public required string SessionToken { get; set; }

        [JsonPropertyName("expire")]
        public required DateTime SessionExpire { get; set; }

    }

    public class LoginResponse
    {
        [JsonPropertyName("entity")]
        public required LoginDetails Details { get; set; }
    }

    public class Group
    {
        [JsonPropertyName("id")]
        public required string GroupId { get; set; }

        [JsonPropertyName("groupName")]
        public required string GroupName { get; set; }
    }

    public class Record
    {
        [JsonPropertyName("id")]
        public string RecordId { get; set; }

        [JsonPropertyName("ownerId")]
        public string OwnerId { get; set; }

        [JsonPropertyName("assetUri")]
        public string AssetURI { get; set; }

        [JsonPropertyName("version")]
        public RecordVersion Version { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("recordType")]
        public string RecordType { get; set; }

        [JsonPropertyName("ownerName")]
        public string OwnerName { get; set; }

        [JsonPropertyName("tags")]
        public HashSet<string> Tags { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("thumbnailUri")]
        public string ThumbnailURI { get; set; }

        [JsonPropertyName("lastModificationTime")]
        public DateTime LastModificationTime { get; set; }

        [JsonPropertyName("creationTime")]
        public DateTime? CreationTime { get; set; }

        [JsonPropertyName("firstPublishTime")]
        public DateTime? FirstPublishTime { get; set; }

        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonPropertyName("isPublic")]
        public bool IsPublic { get; set; }

        [JsonPropertyName("isForPatrons")]
        public bool IsForPatrons { get; set; }

        [JsonPropertyName("isListed")]
        public bool IsListed { get; set; }

        [JsonPropertyName("visits")]
        public int Visits { get; set; }

        [JsonPropertyName("rating")]
        public double Rating { get; set; }

        [JsonPropertyName("randomOrder")]
        public int RandomOrder { get; set; }

        [JsonPropertyName("assetManifest")]
        public List<DBAsset> AssetManifest { get; set; }

        [JsonPropertyName("migrationMetadata")]
        public MigrationMetadata MigrationMetadata { get; set; }
    }
}
