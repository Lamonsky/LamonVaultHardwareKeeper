using Data;
using Data.Helpers;
using WebInterface.Helpers;

namespace WebInterface.Models
{
    public static class GlobalData
    {
        public static int? Id { get; set; } = null;
        public static string Email { get; set; } = null;
        public static string AccessToken { get; set; } = null;
        public static string RefreshToken { get; set; } = null;
        public static string WhichLayout { get; set; } = "_Layout";
        
        public static async void RefreshUserToken()
        {
            RefreshTokenModel refreshToken = new(GlobalData.RefreshToken);
            RefreshTokenModel newuser = await RequestHelper.SendRequestAsync<object, RefreshTokenModel>(URLs.REFRESH, HttpMethod.Post, refreshToken, GlobalData.AccessToken);
            AccessToken = newuser.AccessToken;
            RefreshToken = newuser.RefreshToken;
        }

    }
}
