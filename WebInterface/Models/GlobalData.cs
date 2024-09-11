using Data;
using Data.Helpers;
using System.Net;
using WebInterface.Helpers;

namespace WebInterface.Models
{
    public static class GlobalData
    {
        public static int? Id { get; set; }
        public static string Email { get; set; }
        public static string AccessToken { get; set; }
        public static string RefreshToken { get; set; }
        
        public static async void RefreshUserToken(string _refreshToken)
        {
            RefreshTokenModel refreshToken = new(_refreshToken);
            RefreshTokenModel newuser = await RequestHelper.SendRequestAsync<object, RefreshTokenModel>(URLs.REFRESH, HttpMethod.Post, refreshToken, GlobalData.AccessToken);
            AccessToken = newuser.AccessToken;
            RefreshToken = newuser.RefreshToken;
        }

    }
}
