using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UlutekKioskModels;
using Kiosk_Managment.Models;
using System.Data;

namespace Kiosk_Managment.Controllers
{
    public class ManagementController : Controller
    {
        // GET: Management

        public ActionResult Index()
        {
            List<Publication> publications = new List<Publication>();
            bool rlt = false;
            if (Session["UserItem"] != null)
            {
                rlt = true;
                
                
            }

            DataSet ds = Mysqldb.Select("select * from publications"); 
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Publication pub = new Publication();
                pub.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                pub.TimeOfView = Convert.ToInt32(ds.Tables[0].Rows[i]["TimeOfView"].ToString());
                pub.ExpiryDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ExpiryDate"].ToString());
                pub.ImageData = (byte[])ds.Tables[0].Rows[i]["Image"];
                publications.Add(pub);
            }

            ViewBag.auth = rlt;
            return View(publications);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Publication publication)
        {
            using (Stream istream = publication.Image.InputStream)
            {
                MemoryStream ms = new MemoryStream();
                publication.Image.InputStream.CopyTo(ms);
                byte[] image = ms.ToArray();
                bool rlt = Mysqldb.Insert($"insert into publications(TimeOfView,ExpiryDate,Image) " +
                    $"values('{publication.TimeOfView}','{publication.ExpiryDate.Date:yyyy-MM-dd}','{Convert.ToBase64String(image,Base64FormattingOptions.None)}')");
                if (rlt)
                {
                    ViewBag.Status = true;
                }
            }
            return View();
        }

        public ActionResult Update(int id = 0)
        {
            Publication pub = new Publication();
            TempData["ID"] = id;
            if (id > 0)
            {
                DataSet ds = Mysqldb.Select($"select * from publications where ID={id}");
                if (ds.Tables.Count > 0)
                {
                    pub.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                    pub.TimeOfView = Convert.ToInt32(ds.Tables[0].Rows[0]["TimeOfView"].ToString());
                    pub.ExpiryDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ExpiryDate"].ToString());
                }

                return View(pub);
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult Update(Publication publication)
        {
            bool rlt = Mysqldb.Update($"update publications set TimeOfView='{publication.TimeOfView}',ExpiryDate='{publication.ExpiryDate:yyyy-MM-dd}' where ID='{publication.ID}'");
            if (rlt)
            {
                ViewBag.Status = true;
                TempData["ID"] = publication.ID;
            }
            return View(publication);
        }

        public ActionResult Delete(int id)
        {
            Mysqldb.Delete($"delete from publications where ID={id}");
            return RedirectToAction("Index", "Management");
        }
    }
}