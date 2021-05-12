using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Media.Imaging;

namespace UlutekKioskModels
{
    public class PublicationBase
    {
        public static string PublicationOnChange { get; set; }
        public int ID { get; set; }
        public int TimeOfView { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
