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
    public class KhoaHocController : Controller
    {
        // GET: Admin/KhoaHoc
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetList(int draw, int start, int length, FormCollection F)
        {
            var SearchKey = F["search[value]"];
            KhoaHocDBContext db = new KhoaHocDBContext();
            int total = db.GetTotalRecords();
            List<KhoaHoc> obj = db.GetPagedData(start, length, SearchKey);
            //var KhoaHocTbl = db.GetKhoaHocs().ToList();
            //ViewBag.KhoaHocTbl = new SelectList(KhoaHocTbl, "maKhoa", "tenNganh");

            return Json(new { draw, recordsTotal = total, recordsFiltered = total, data = obj }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            KhoaHoc db = new KhoaHoc();
            HDTDBContext ds = new HDTDBContext();
            ViewBag.countrydrop = ds.GetHeDaoTaos().Select(x => new SelectListItem { Text = x.MaHDT, Value = x.TenHDT.ToString() }).ToList();
            return View(db);
        }

        [HttpPost]
        public ActionResult Create(KhoaHoc KhoaHoc)
        {

            try
            {
                if (ModelState.IsValid == true)
                {
                    KhoaHocDBContext db = new KhoaHocDBContext();
                    bool check = db.DddKhoaHoc(KhoaHoc);
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

        public ActionResult Edit(string MaKH)
        {
            KhoaHocDBContext db = new KhoaHocDBContext();
            //KhoaHoc db = new KhoaHoc();
            HDTDBContext ds = new HDTDBContext();
            ViewBag.countrydrop = ds.GetHeDaoTaos().Select(x => new SelectListItem { Text = x.MaHDT, Value = x.TenHDT.ToString() }).ToList();
            var row = db.GetKhoaHocs().Find(model => model.MaKH == MaKH);
            return View(row);
        }

        [HttpPost]
        public ActionResult Edit(string MaKH, KhoaHoc KhoaHoc)
        {
            if (ModelState.IsValid == true)
            {
                KhoaHocDBContext db = new KhoaHocDBContext();
                bool check = db.UpdateKhoaHoc(KhoaHoc);
                if (check == true)
                {
                    TempData["UpdateMessage"] = "Data has been Updated Successfully.";
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }

            }

            return View();
        }

        public ActionResult Delete(string MaKH)
        {
            KhoaHocDBContext context = new KhoaHocDBContext();
            var row = context.GetKhoaHocs().Find(model => model.MaKH == MaKH);
            return View(row);
        }

        [HttpPost]
        public ActionResult Delete(string MaKH, KhoaHoc KhoaHoc)
        {

            KhoaHocDBContext context = new KhoaHocDBContext();
            bool check = context.DeleteKhoaHoc(MaKH);
            if (check == true)
            {
                TempData["DeleteMessage"] = "Data has been Deleted Successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}