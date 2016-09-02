using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.Extensions.Options;
using System.Xml.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Dynamic;
using Loyalty.Models;
using Loyalty.Repository;
using Microsoft.AspNetCore.Session;
using System.Globalization;
using Newtonsoft.Json;

namespace Loyalty.Controllers
{

    public class UserController : Controller
    {
        #region Constructor      

        UserRepository _user = new UserRepository();
        #endregion


        public IActionResult Account()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }




        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetUser()
        {
            string userid = HttpContext.Session.GetString("UserID");
            var jsonSerialiser = JsonConvert.SerializeObject(_user.GetUserByID(userid));
            return Json(jsonSerialiser);
        }


        [HttpPost]
        [AllowAnonymous]
        public void AddUser(Users user)
        {
            double dbsize = _user.Getdbsize();
            if (dbsize >= 4.50)
            {
            }

            if (user.EmailID != null)
            {
                List<Users> lstUser = _user.GetUserByMailID(user.EmailID);
                if (lstUser.Count == 0)
                {
                    _user.Register(user);
                }
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public void UpdateUser(Users _User)
        {
            double dbsize = _user.Getdbsize();
            if (dbsize >= 4.50)
            {               
            }

            if (_User.UserID != null)
            {             
              _user.UpdateUser(_User);
                //TempData["userinfo"] = "User details updated successfully...";
            }          
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult LoginUser(Login _login)
        {
            _user.CreateUserTable();
            _user.CreateMembershipTable();
            List<Login> _usrlogin = _user.GetLogin(_login);

            if (_usrlogin != null && _usrlogin.Count > 0)
            {
                if (_usrlogin[0].UserID != null)
                {
                    HttpContext.Session.SetString("UserID", _usrlogin[0].UserID.ToString());
                    HttpContext.Session.SetString("UserName", _usrlogin[0].FirstName.ToString() + ' ' + _usrlogin[0].LastName.ToString());
                }
            }
            var jsonSerialiser = JsonConvert.SerializeObject(_usrlogin);
            return Json(jsonSerialiser);
        }


        public IActionResult Login(Login _login)
        {
            return View();
        }
        public IActionResult SessionTimeOut()
        {
            HttpContext.Session.Clear();
            return View();
        }


        public IActionResult UserName()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return Content("Welcome " + HttpContext.Session.GetString("UserName").ToString());
            }
            else
                return Content("");
        }



        public IActionResult Logout()
        {
            if (ModelState.IsValid)
                TempData["notice"] = "Successfully Logged out.";
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "User");
        }
    }
}