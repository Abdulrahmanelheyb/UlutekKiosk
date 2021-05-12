using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Media.Imaging;
using UlutekKioskModels;

namespace UlutekKiosk.Models
{
    public class Publication: PublicationBase
    {
        public BitmapImage Image { get; set; }
    }
}
