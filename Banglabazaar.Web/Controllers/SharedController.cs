using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Banglabazaar.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SharedController : Controller
    {
       public JsonResult uploadimage()
        {
            JsonResult Result = new JsonResult();
            Result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var file = Request.Files[0];
                var filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/"), filename);
                file.SaveAs(path);
                Result.Data = new { Success = true, ImageURL = string.Format("/Content/images/{0}",filename) };
            }
            catch (Exception ex)
            {

                Result.Data = new { Success = false, Message =ex.Message };
            }
            return Result;
                
        }
       
    }
}