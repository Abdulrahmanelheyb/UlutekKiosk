using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlutekKioskModels
{
    internal static class Globals
    {
        public static readonly string ConnectionString = $"Server={Properties.Settings.Default.IP};Database=ulutek_kiosk;Uid=ulutek;Pwd=Ulutek_0;";
    }
}
