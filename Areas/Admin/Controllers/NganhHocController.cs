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
    public class NganhHocController : Controller
    {
        // GET: Admin/NganhHoc
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetList(int draw, int start, int length, FormCollection F)
        {
            var SearchKey = F["search[value]"];
            NganhHocDBContext db = new NganhHocDBContext();
            int total = db.GetTotalRecords();
            List<NganhHoc> obj = db.GetPagedData(start, length, SearchKey);
            //var NganhHocTbl = db.GetNganhHocs().ToList();
            //ViewBag.NganhHocTbl = new SelectList(NganhHocTbl, "maKhoa", "tenNganh");

            return Json(new { draw, recordsTotal = total, recordsFiltered = total, data = obj }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            NganhHoc db = new NganhHoc();
            KhoaDBContext ds = new KhoaDBContext();
            ViewBag.countrydrop = ds.GetKhoas().Select(x => new SelectListItem { Text = x.tenKhoa, Value = x.maKhoa.ToString() }).ToList();
            return View(db);
        }

        [HttpPost]
        public ActionResult Create(NganhHoc NganhHoc)
        {

            try
            {
                if (ModelState.IsValid == true)
                {
                    NganhHocDBContext db = new NganhHocDBContext();
                    bool check = db.DddNganhHoc(NganhHoc);
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

        public ActionResult Edit(string maNganhHoc)
        {
            NganhHocDBContext db = new NganhHocDBContext();
            //NganhHoc db = new NganhHoc();
            KhoaDBContext ds = new KhoaDBContext();
            ViewBag.countrydrop = ds.GetKhoas().Select(x => new SelectListItem { Text = x.tenKhoa, Value = x.maKhoa.ToString() }).ToList();
            var row = db.GetNganhHocs().Find(model => model.maNganhHoc == maNganhHoc);
            return View(row);
        }

        [HttpPost]
        public ActionResult Edit(string maNganhHoc, NganhHoc NganhHoc)
        {
            if (ModelState.IsValid == true)
            {
                NganhHocDBContext db = new NganhHocDBContext();
                bool check = db.UpdateNganhHoc(NganhHoc);
                if (check == true)
                {
                    TempData["UpdateMessage"] = "Data has been Updated Successfully.";
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }

            }

            return View();
        }

        public ActionResult Delete(string maNganhHoc)
        {
            NganhHocDBContext context = new NganhHocDBContext();
            var row = context.GetNganhHocs().Find(model => model.maNganhHoc == maNganhHoc);
            return View(row);
        }

        [HttpPost]
        public ActionResult Delete(string maNganhHoc, NganhHoc NganhHoc)
        {

            NganhHocDBContext context = new NganhHocDBContext();
            bool check = context.DeleteNganhHoc(maNganhHoc);
            if (check == true)
            {
                TempData["DeleteMessage"] = "Data has been Deleted Successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}

