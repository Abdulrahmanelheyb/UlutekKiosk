using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UlutekKioskModels;
using Kiosk_Managment.Models;
using System.Data;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace Kiosk_Managment.Controllers
{
    public class ManagementController : Controller
    {

        private bool PublicationsOnChange(int ObjectHashCode)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string hashedcode = Cryption.Encrypt(sha256, Convert.ToBase64String(BitConverter.GetBytes(ObjectHashCode)));
                return Mysqldb.Update($"update tables_onchange set Value='{hashedcode}' where TableName='publications'");
            }
        }

        public ActionResult Index()
        {
            List<Publication> publications = new List<Publication>();
            bool rlt = false;
            if (Session["UserItem"] != null)
            {
                rlt = true;
            }

            DataSet ds = Mysqldb.Select($"select * from publications where ExpiryDate>='{DateTime.Now.Date:yyyy-MM-dd}' order by ExpiryDate"); 
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
            string Message;
            if (publication.Image != null)
            {
                using (Stream istream = publication.Image.InputStream)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        publication.Image.InputStream.CopyTo(ms);
                        byte[] image = ms.ToArray();

                        var Parameters = new List<MySqlParameter>();
                        string TimeOfViewParam = "default";
                        if (publication.TimeOfView > 0)
                        {
                            TimeOfViewParam = "@tov";
                            Parameters.Add(new MySqlParameter("@tov", publication.TimeOfView));
                        }

                        if (publication.ExpiryDate < DateTime.Now.Date)
                        {
                            ViewBag.Status = false;
                            ViewBag.StatusMessage = Message = "Please input Expiry Date.";
                            return View();
                        }

                        Parameters.AddRange(new MySqlParameter[]
                        {
                            new MySqlParameter("@expdate", publication.ExpiryDate.Date.ToString("yyyy-MM-dd")),
                            new MySqlParameter("@image", image)
                        });
                        if (Mysqldb.Insert($"insert into publications(TimeOfView,ExpiryDate,Image) values({TimeOfViewParam},@expdate,@image)", Parameters))
                        {
                            ViewBag.Status = true;
                            ViewBag.StatusMessage = Message = "Publication Added Successfully.";
                            PublicationsOnChange(publication.GetHashCode());
                        }
                    }
                    
                }
            }
            else
            {
                ViewBag.Status = false;
                Message = "Publication adding failed.";
                ViewBag.StatusMessage = Message;
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
                    PublicationsOnChange(pub.GetHashCode());
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
                PublicationsOnChange(publication.GetHashCode());
            }
            return View(publication);
        }

        public ActionResult Delete(int id)
        {
            Mysqldb.Delete($"delete from publications where ID={id}");
            Publication pub = new Publication
            {
                ID = id
            };
            PublicationsOnChange(pub.GetHashCode());
            return RedirectToAction("Index", "Management");
        }
    }
}