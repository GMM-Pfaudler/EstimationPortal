using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EstimationPortal.CustomeAttribute;
using SUP_CORE.EFModel;
using SUP_DAL.EFContextProvider;

namespace EstimationPortal.Controllers
{
    [SessionTimeout]
    [NoDirectAccess]
    public class EnqStatusMasterController : Controller
    {
        private DropDownCollection _dropDownCollection;

        private readonly SupplierDbContext _db;
        public EnqStatusMasterController(SupplierDbContext db, DropDownCollection dropDownCollection)
        {
            _dropDownCollection = dropDownCollection;
            _db = db;
        }
        #region Enquiry Status Master
        [HttpGet]
        public ActionResult Index()
        {
            var result = _db.Tbl_EnqStatusMasters.OrderByDescending(s => s.Id).ToList();
            return View(result);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tbl_EnqStatusMasters mm)
        {
            if (ModelState.IsValid)
            {
                bool? dbdata = _dropDownCollection.CheckEnquiStatusExist(mm.EnqStatus.Trim(), 0);
                if (dbdata == false)
                {
                    mm.IsDelete = false;
                    mm.CreatedDate = DateTime.Now;
                    mm.CreatedBy = Session["UserID"]?.ToString(); // Adjust based on your auth logic

                    _db.Tbl_EnqStatusMasters.Add(mm);
                    _db.SaveChanges();

                    TempData["Success"] = "Enquiry Status created successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "This Enquiry Status already exists!");
                    return View(mm);
                }

            }

            return View(mm);
        }

        public ActionResult Edit(int id)
        {
            var user = _db.Tbl_EnqStatusMasters.Find(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tbl_EnqStatusMasters mm, int id)
        {
            if (ModelState.IsValid)
            {
                bool? dbdata = _dropDownCollection.CheckEnquiStatusExist(mm.EnqStatus.Trim(), id);
                if (dbdata == false)
                {
                    var existingUser = _db.Tbl_EnqStatusMasters.Find(mm.Id);
                    if (existingUser != null)
                    {
                        existingUser.EnqStatus = mm.EnqStatus.Trim();
                        existingUser.IsDelete = mm.IsDelete;
                        // Meta Data
                        existingUser.ModifiedDate = DateTime.Now;
                        existingUser.ModifiedBy = Session["UserID"]?.ToString();

                        _db.Entry(existingUser).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();

                        TempData["Success"] = "Enquiry Status updated successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        this.AddNotification("Enquiry Status not found!", "Error");
                        return View(mm);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "This Enquiry Status already exists!");
                    return View(mm);
                }

            }


            return View(mm);
        }
        #endregion
    }
}