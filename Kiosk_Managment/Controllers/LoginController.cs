using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiosk_Managment.Models;
using UlutekKioskModels;

namespace Kiosk_Managment.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            if (user.Email == null || user.Pwd == null)
            {
                return View();
            }

            string email = user.Email;
            string pwd = user.Pwd;
            DataSet ds = Mysqldb.Select("select * from users where Email='" + email + "'");

            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int dbid = (int)ds.Tables[0].Rows[0][0];
                    string dbpwd = ds.Tables[0].Rows[0][3].ToString();
                    string dbusrname = ds.Tables[0].Rows[0][1].ToString();
                    if (dbpwd == pwd)
                    {
                        user.ID = dbid;
                        user.Username = dbusrname;
                        Session.Timeout = 30;
                        Session["UserItem"] = user;

                        return RedirectToAction("Index", "Management");
                    }
                }
            }
            return View();
        }
    }
}