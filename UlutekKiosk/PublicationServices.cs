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
            // This method gets only continues publications
            List<Publication> rlt = new List<Publication>();

            //This Get publications with sql query.
            DataSet ds = Mysqldb.Select($"select * from publications where ExpiryDate>='{DateTime.Now.Date:yyyy-MM-dd}'");

            //This Loop on dataset table rows.
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //This Get image bytes.
                byte[] imgbyte = (byte[])ds.Tables[0].Rows[i]["Image"];

                //This create instance MemoryStream with give image bytes.
                using (MemoryStream ms = new MemoryStream(imgbyte))
                {
                    //This create new instance of BitmapImage and initialize image bytes.
                    var imageSource = new BitmapImage
                    {
                        CreateOptions = BitmapCreateOptions.PreservePixelFormat
                    };
                    imageSource.BeginInit();
                    imageSource.StreamSource = ms;
                    imageSource.CacheOption = BitmapCacheOption.OnLoad;
                    imageSource.EndInit();

                    //This create new instance of publication and set properties.
                    Publication pub = new Publication
                    {
                        ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
                        Image = imageSource,
                        TimeOfView = Convert.ToInt32(ds.Tables[0].Rows[i]["TimeOfView"]),
                        ExpiryDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ExpiryDate"])
                    };

                    // Add to list to return
                    rlt.Add(pub);
                }
                
            }

            // Get Publications On Change Hashcode.
            PublicationBase.PublicationOnChange =
                Mysqldb.Select("select * from tables_onchange where TableName='publications'").Tables[0].Rows[0]["Value"].ToString();

            return rlt;
        }

        //This method check on change status.
        public static string OnChange()
        {
            string rlt = null;
            //Get publications table hashcode.
            DataSet ds = Mysqldb.Select("select * from tables_onchange where TableName='publications'");

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (PublicationBase.PublicationOnChange == null)
                {
                    PublicationBase.PublicationOnChange = ds.Tables[0].Rows[0]["Value"].ToString();
                }
                else
                {
                    string poc = ds.Tables[0].Rows[0]["Value"].ToString();
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
