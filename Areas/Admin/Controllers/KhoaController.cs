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
    public class KhoaController : Controller
    {
        // GET: Admin/Khoa
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetList(int draw, int start, int length, FormCollection F)
        {
            var SearchKey = F["search[value]"];
            KhoaDBContext db = new KhoaDBContext();
            
            int total = db.GetTotalRecords();
            List<Khoa> obj = db.GetPagedData(start, length, SearchKey);

            return Json(new { draw, recordsTotal = total, recordsFiltered = total, data = obj }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            Khoa db = new Khoa();
            return View(db);
        }

        [HttpPost]
        public ActionResult Create(Khoa khoa)
        {

            try
            {
                if (ModelState.IsValid == true)
                {
                    KhoaDBContext db = new KhoaDBContext();
                    bool check = db.DddKhoa(khoa);
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

        public ActionResult Edit(string maKhoa)
        {
            KhoaDBContext db = new KhoaDBContext();
            var row = db.GetKhoas().Find(model => model.maKhoa == maKhoa);
            return View(row);
        }

        //public ActionResult Edit(string maKhoa, string tenKhoa)
        //{
        //    KhoaDBContext db = new KhoaDBContext();
        //    db.UpdateKhoa(maKhoa, tenKhoa);

        //    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public ActionResult Edit(string maKhoa, Khoa khoa)
        {
            if (ModelState.IsValid == true)
            {
                KhoaDBContext db = new KhoaDBContext();
                bool check = db.UpdateKhoa(khoa);
                if (check == true)
                {
                    TempData["UpdateMessage"] = "Data has been Updated Successfully.";
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }

            }

            return View();
        }

        public ActionResult DeleteKhoa(string maKhoa)
        {
            KhoaDBContext context = new KhoaDBContext();
            var row = context.GetKhoas().Find(model => model.maKhoa == maKhoa);
            return View(row);
        }

        [HttpPost]
        public ActionResult DeleteKhoa(string maKhoa, Khoa khoa)
        {

            KhoaDBContext context = new KhoaDBContext();
            bool check = context.DeleteKhoa(maKhoa);
            if (check == true)
            {
                TempData["DeleteMessage"] = "Data has been Deleted Successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}