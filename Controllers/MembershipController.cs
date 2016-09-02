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
using Newtonsoft.Json;
 
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Loyalty.Controllers
{
    public class MembershipController : Controller
    {
        MembershipRepository _membership = new MembershipRepository();
        UserRepository _user = new UserRepository();
        // GET: /<controller>/

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {              
                return RedirectToAction("Login", "User");
            }      
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetMembership()
        {
            string userid = HttpContext.Session.GetString("UserID");
            var jsonSerialiser = JsonConvert.SerializeObject(_membership.GetMembership(userid));
            return Json(jsonSerialiser);
        }



        [HttpPost]
        [AllowAnonymous]
        public void AddMembership(Membership memship)
        {
            double dbsize = _user.Getdbsize();
            if (dbsize >= 4.50)
            {
            }
            if (memship.MemberID != null)
            {
                string userid = HttpContext.Session.GetString("UserID");
                _membership.AddMembership(memship, userid);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Create(Membership memship)
        {
            return View();
        }        

        [HttpPost]
        [AllowAnonymous]
        public void UpdateMembership(Membership memship)
        {
            double dbsize = _user.Getdbsize();
            if (dbsize >= 4.50)
            {
            }
            if (memship.MembershipID != null)
            {
                _membership.UpdateMembership(memship);
            }
        }

        public IActionResult Edit()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {                
                return RedirectToAction("Login", "User");
            }
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetMemberbyID(Membership memship)
        {
            var jsonSerialiser = JsonConvert.SerializeObject(_membership.GetMembershipbyID(memship.MembershipID));
            return Json(jsonSerialiser);
        }
    }
}
