using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Banglabazaar.Services;
using Banglabazaar.Web.HomeViewModel;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace Banglabazaar.Web.Controllers
{
   
    public class OrderController : Controller
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


        Orderservices Orderservice = new Orderservices();
        Configservices configservice = new Configservices();
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string UserID, string status, int? pageNo)
        {
            OrderViewModel model = new OrderViewModel();
         
            model.UserID = UserID;
            model.Status = status;
            var pageSize = configservice.PageSize();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.Orders = Orderservice.searchingOrders(UserID,status,pageNo.Value,pageSize);
         
            var totalrecords = Orderservice.getOrderscount(UserID,status);

           
            model.Pager = new pager(totalrecords, pageNo, pageSize);
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Detail(int ID)
        {


            OrderDetailViewModel Model = new OrderDetailViewModel();
            Model.Order = Orderservice.getOrderDetail(ID);
            if (Model.Order != null)
            {
                Model.OrderBy = UserManager.FindById(Model.Order.UserId);
            }
            Model.availablestatus = new List<string> { "Pending", "In Progress", "Delivered" };
            return View(Model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult changestatus(string status,int ID)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            OrderDetailViewModel Model = new OrderDetailViewModel();
            Model.Order = Orderservice.getOrderDetail(ID);
            result.Data = new { Success = Orderservice.UpdateOrderStatus(status,ID) };

            return result;
        }
        [Authorize]
        public ActionResult IndexByUser(string UserID, string status, int? pageNo)
        {
            OrderViewModel model = new OrderViewModel();
           
            model.UserID = UserID;
            model.Status = status;
            var pageSize = configservice.PageSize();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.Orders = Orderservice.searchingOrdersByUser(UserID, status, pageNo.Value, pageSize);

            var totalrecords = Orderservice.getOrderscountByUser(UserID, status);


            model.Pager = new pager(totalrecords, pageNo, pageSize);
            return View(model);
        }
        [Authorize]
        public ActionResult DetailByUser(int ID)
        {


            OrderDetailViewModel Model = new OrderDetailViewModel();
            Model.Order = Orderservice.getOrderDetailByUser(ID);
            if (Model.Order != null)
            {
                Model.OrderBy = UserManager.FindById(Model.Order.UserId);
            }
            Model.availablestatus = new List<string> { "Pending", "In Progress", "Delivered" };
            return View(Model);
        }

    }
}