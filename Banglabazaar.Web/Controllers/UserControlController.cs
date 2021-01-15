using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bangalabazaar_Entities;
using Banglabazaar.Web.HomeViewModel;
using Banglabazaar.Database;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Banglabazaar.Services;
using System.IO;


namespace Banglabazaar.Web.Controllers
{
 
    public class UserControlController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;



        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        Configservices config = new Configservices();
        BBcontext context = new BBcontext();
        [Authorize]
        public ActionResult UpdateProfile()
        {
            UserControlModel model = new UserControlModel();
            var user = UserManager.FindById(User.Identity.GetUserId());
            model.UserID = User.Identity.GetUserId();
            model.Name = user.Name;
            model.Email = user.Email;
            model.Address = user.Address;


          
            
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public ActionResult UpdateProfile(UserControlModel model)
        {
            if (model != null)
            {

            
            if (ModelState.IsValid)
            {
                var user = UserManager.FindById(User.Identity.GetUserId());
                user.Id = model.UserID;
                user.Name = model.Name;
                user.Address = model.Address;
                UserManager.Update(user);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                    return new HttpStatusCodeResult(500);
                }
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }







        }
        [Authorize(Roles ="Admin")]
        public ActionResult Configuration()
        {
            configviewmodel model = new configviewmodel();
            model.Config = context.Configs.ToList();

            return View(model);
        }
        public ActionResult Configurationupdate(string key)
        {
            configviewlModel model = new configviewlModel();
            model.Config = context.Configs.Single(x => x.Key == key);
            
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Configurationupdate(string Key,string Value)
        {
            config.UpdateConfig(Key, Value);
            return RedirectToAction("Configuration");
        }




    }
}