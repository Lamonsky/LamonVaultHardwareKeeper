using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministrationApp.Login
{
    internal interface IPasswordProvider
    {
        string GetPassword();
    }
}
