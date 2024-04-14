using System.IO;
using Windows.Security.Cryptography;
using Windows.Storage;
using Windows.Storage.Provider;
using SFFileLib;

namespace SFSFront
{
    public partial class SFSGUI : Form
    {
        StorageProviderSyncRootInfo resSyncRootInfo;
        public SFSGUI()
        {
            InitializeComponent();

        }
        private async void SFSGUI_Load(object sender, EventArgs e)
        {
            string path = "C:\\Users\\thing\\ResSync";
            resSyncRootInfo = new StorageProviderSyncRootInfo
            {
                Id = "Resonite!S-1-5-32-545!U-Lexevo",
                Path = await StorageFolder.GetFolderFromPathAsync(path),
                AllowPinning = true,
                HydrationPolicy = StorageProviderHydrationPolicy.Full,
                DisplayNameResource = "Resonite",
                IconResource = "E:\\Downloads\\RSN_Logomark_Color.ico",
                Context = CryptographicBuffer.ConvertStringToBinary(path, BinaryStringEncoding.Utf8),

                InSyncPolicy = StorageProviderInSyncPolicy.Default
            };
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            StorageProviderSyncRootManager.Register(resSyncRootInfo);
        }

        private void unregisterButton_Click(object sender, EventArgs e)
        {
            StorageProviderSyncRootManager.Unregister("Resonite!S-1-5-32-545!U-Lexevo");
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            AccountInfo account = new AccountInfo(UsernameTextbox.Text, PasswordTextbox.Text);
            AccountInfo loggedin = await AccountInfo.GetGroups(await AccountInfo.Login(account));
        }
    }
}
