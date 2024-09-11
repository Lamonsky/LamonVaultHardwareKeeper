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

    }
}
