using QLSV.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
namespace QLSV.Areas.Admin.Controllers
{
    public class HeDaoTaoController : Controller
    {
        // GET: Admin/HeDaoTao
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetList(int draw, int start, int length, FormCollection F)
        {
            var SearchKey = F["search[value]"];
            HDTDBContext db = new HDTDBContext();
            int total = db.GetTotalRecords();
            List<HeDaoTao> obj = db.GetPagedData(start, length, SearchKey);

            return Json(new { draw, recordsTotal = total, recordsFiltered = total, data = obj }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            HeDaoTao db = new HeDaoTao();
            return View(db);
        }

        [HttpPost]
        public ActionResult Create(HeDaoTao HeDaoTao)
        {

            try
            {
                if (ModelState.IsValid == true)
                {
                    HDTDBContext db = new HDTDBContext();
                    bool check = db.DddHeDaoTao(HeDaoTao);
                    if (check == true)
                    {
                        TempData["InsertMessage"] = "Data has been Inserted Successfully.";
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }

                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(string MaHDT)
        {
            HDTDBContext db = new HDTDBContext();
            var row = db.GetHeDaoTaos().Find(model => model.MaHDT == MaHDT);
            return View(row);
        }

        [HttpPost]
        public ActionResult Edit(string MaHDT, HeDaoTao HeDaoTao)
        {
            if (ModelState.IsValid == true)
            {
                HDTDBContext db = new HDTDBContext();
                bool check = db.UpdateHeDaoTao(HeDaoTao);
                if (check == true)
                {
                    TempData["UpdateMessage"] = "Data has been Updated Successfully.";
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }

            }

            return View();
        }

        public ActionResult Delete(string MaHDT)
        {
            HDTDBContext context = new HDTDBContext();
            var row = context.GetHeDaoTaos().Find(model => model.MaHDT == MaHDT);
            return View(row);
        }

        [HttpPost]
        public ActionResult Delete(string MaHDT, HeDaoTao HeDaoTao)
        {

            HDTDBContext context = new HDTDBContext();
            bool check = context.DeleteHeDaoTao(MaHDT);
            if (check == true)
            {
                TempData["DeleteMessage"] = "Data has been Deleted Successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
