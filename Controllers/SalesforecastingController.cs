using Newtonsoft.Json;
using SpauldingRidgeTask.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpauldingRidgeTask.Controllers
{
    public class SalesforecastingController : Controller
    {
        private SalesforecastingModel salesforecastingModel;
        public SalesforecastingController()
        {
            salesforecastingModel = new SalesforecastingModel();
        }
        public ActionResult TotalSales()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetSalesList(string Year)
        {
            return Json(JsonConvert.SerializeObject(salesforecastingModel.GetSalesData(Year)), JsonRequestBehavior.AllowGet);
        }
        public ActionResult forecastedSales()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetforecastedSalesList(int year, float perc_)
        {
            return Json(JsonConvert.SerializeObject(salesforecastingModel.Getforecasted_SalesData(year, perc_)), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetSpecficStateData(string State, int Year, float percentage)
        {
            return Json(JsonConvert.SerializeObject(salesforecastingModel.Getforecasted_StateData(State, Year, percentage)), JsonRequestBehavior.AllowGet);
        }

    }
}