using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banglabazaar.Web.HomeViewModel
{
    public class BaseListingViewModel
    {
    }

  

    public class pager
    {

        public pager(int totalitems, int? page,int pagesize=10)
        {
            if (pagesize == 0) pagesize = 10;
            var totalpages = (int)Math.Ceiling((decimal)totalitems / (decimal)pagesize);
            var currentpage = page != null ? (int)page : 1;
            var startpage = currentpage - 5;
            var endpage = currentpage + 4;
            if (startpage<= 0)
            {
                endpage -= (startpage - 1);
                startpage = 1;
            }
            if (endpage > totalpages)
            {
                endpage = totalpages;
                if(endpage > 10)
                {
                    startpage = endpage - 9;
                }
            }

            TotalItems = totalitems;
            CurrentPage = currentpage;
            StartPage = startpage;
            EndPage = endpage;
            PageSize = pagesize;
            TotalPages = totalpages;


       }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }


    }
}