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
    public class EmailMasterController : Controller
    {
        private DropDownCollection _dropDownCollection;

        private readonly SupplierDbContext _db;
        public EmailMasterController(SupplierDbContext db, DropDownCollection dropDownCollection)
        {
            _dropDownCollection = dropDownCollection;
            _db = db;
        }
        #region Email Master
        [HttpGet]
        public ActionResult Index()
        {
            var result = _db.Tbl_EmailMasters.OrderByDescending(s => s.Id).ToList();
            return View(result);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tbl_EmailMasters mm)
        {
            if (ModelState.IsValid)
            {
                bool? dbdata = _dropDownCollection.CheckEmailIDExist(mm.Email_ID.Trim(), 0);
                if (dbdata == false)
                {
                    mm.IsDelete = false;
                    mm.CreatedDate = DateTime.Now;
                    mm.CreatedBy = Session["UserID"]?.ToString(); // Adjust based on your auth logic

                    _db.Tbl_EmailMasters.Add(mm);
                    _db.SaveChanges();

                    TempData["Success"] = "Record created successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "This Email ID already exist !");
                    return View(mm);
                }

            }

            return View(mm);
        }


        public ActionResult Edit(int id)
        {
            var user = _db.Tbl_EmailMasters.Find(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tbl_EmailMasters mm, int id)
        {
            if (ModelState.IsValid)
            {
                bool? dbdata = _dropDownCollection.CheckEmailIDExist(mm.Email_ID.Trim(), id);
                if (dbdata == false)
                {
                    var existingUser = _db.Tbl_EmailMasters.Find(mm.Id);
                    if (existingUser != null)
                    {
                        existingUser.Name = mm.Name.Trim();
                        existingUser.Email_ID = mm.Email_ID.Trim();
                        existingUser.IsDelete = mm.IsDelete;
                        // Meta Data
                        existingUser.ModifiedDate = DateTime.Now;
                        existingUser.ModifiedBy = Session["UserID"]?.ToString();

                        _db.Entry(existingUser).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();

                        TempData["Success"] = "Record updated successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        this.AddNotification("Record not found!", "Error");
                        return View(mm);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "This Email ID already exist !");
                    return View(mm);
                }

            }


            return View(mm);
        }
        #endregion
    }
}