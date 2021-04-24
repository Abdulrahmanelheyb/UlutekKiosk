using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace UlutekKiosk.OAL
{
    public class Publication
    {
        public BitmapImage Image { get; set; }
        public int TimeOfView { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
