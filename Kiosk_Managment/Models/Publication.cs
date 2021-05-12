using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Media.Imaging;
using UlutekKioskModels;

namespace Kiosk_Managment.Models
{
    public class Publication : PublicationBase
    {
        public HttpPostedFileBase Image { get; set; }
        public byte[] ImageData { get; set; }
    }
}
