using Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministrationApp.Helpers
{
    public static class GlobalData
    {
        public static string AccessToken { get; set; } = null;
        public static string RefreshToken { get; set; } = null;
        public static string Email { get; set; } = null;
        public static int UserId { get; set; }          

    }
}
