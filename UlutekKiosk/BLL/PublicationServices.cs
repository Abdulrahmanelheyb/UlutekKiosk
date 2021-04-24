using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using UlutekKiosk.OAL;

namespace UlutekKiosk.BLL
{
    public class PublicationServices
    {
        public static List<Publication> GetPublications()
        {
            List<Publication> rlt = new List<Publication>();

            var files = Directory.GetFiles("images");
            Array.Sort(files, StringComparer.Ordinal);
            for (int i = 0; i < files.Length; i++)
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                Stream bmpStream = File.Open($"{files[i]}", FileMode.Open);
                bi.StreamSource = bmpStream;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();

                Publication pub = new Publication
                {
                    Image = bi,
                    TimeOfView = 10
                };

                #region Expiry Date Algorithm
                files[i] = files[i].Replace(".jpg", "");
                files[i] = files[i].Replace("images\\", "");
                DateTime expdate;
                bool isparsed = DateTime.TryParse(files[i], out expdate);
                if (isparsed)
                {
                    pub.ExpiryDate = expdate;
                }

                #endregion

                rlt.Add(pub);
            }

            return rlt;
        }

    }
}
