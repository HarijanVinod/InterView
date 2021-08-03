using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InterviewDemo;
using InterviewDemo.Models.BO;

namespace InterviewDemo.Controllers
{
    public class LoadLossTypeDetailsController : BaseController
    {
        // GET: LoadLossTypeDetail
        [HttpGet]
        public ActionResult GetLossTypeData()
        {
            try
            {
                return View(new LoadLossTypeDetailsBO().GetLossTypeData(1));
            }
            catch (Exception ex)
            {

                LogHelper.LogException(ex);
                ViewBag.RouteMessage = ex.Message;
                return null;
            }
            
        }

        [HttpPost]
        public ActionResult GetLossTypeData(int currentPageIndex)
        {
            try
            {
                ViewBag.currentPageIndex = currentPageIndex;
                return View(new LoadLossTypeDetailsBO().GetLossTypeData(currentPageIndex));
                
            }
            catch (Exception ex)
            {

                LogHelper.LogException(ex);
                ViewBag.RouteMessage = ex.Message;
                return null;
            }
            
        }

    }
}