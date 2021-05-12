using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using UlutekKiosk.Models;
using UlutekKioskModels;

namespace UlutekKiosk
{
    public class PublicationServices
    {
        public static List<Publication> GetPublications()
        {
            // this method gets only continues publications
            List<Publication> rlt = new List<Publication>();

            DataSet ds = Mysqldb.Select($"select * from publications where ExpiryDate>'{DateTime.Now.Date:yyyy-MM-dd}'");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                byte[] imgbyte = (byte[])ds.Tables[0].Rows[i]["Image"];
                using (MemoryStream ms = new MemoryStream(imgbyte))
                {
                    var imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.StreamSource = ms;
                    imageSource.CacheOption = BitmapCacheOption.OnLoad;
                    imageSource.EndInit();

                    Publication pub = new Publication
                    {
                        ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
                        Image = imageSource,
                        TimeOfView = Convert.ToInt32(ds.Tables[0].Rows[i]["TimeOfView"]),
                        ExpiryDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ExpiryDate"])
                    };

                    rlt.Add(pub);
                }
                
            }

            PublicationBase.PublicationOnChange =
                Mysqldb.Select("select * from tables_onchange where TableName='publications'").Tables[0].Rows[0]["Value"].ToString();

            return rlt;
        }

        public static bool DeleteExpiredImage(int id)
        {
            bool rlt = Mysqldb.Delete($"delete from publications where ID={id}");
            return rlt;
        }

        public static string OnChange()
        {
            string rlt = null;
            DataSet ds = Mysqldb.Select("select * from tables_onchange where TableName='publications'");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (PublicationBase.PublicationOnChange == null)
                {
                    PublicationBase.PublicationOnChange = ds.Tables[0].Rows[i]["Value"].ToString();
                }
                else
                {
                    string poc = ds.Tables[0].Rows[i]["Value"].ToString();
                    if (poc != PublicationBase.PublicationOnChange)
                    {
                        rlt = poc;
                    }
                }
            }
            return rlt;
        }

    }
}
