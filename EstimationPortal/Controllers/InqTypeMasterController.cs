using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EstimationPortal.CustomeAttribute;
using SUP_BAL.IRepository;
using SUP_CORE.EFModel;
using SUP_DAL.EFContextProvider;

namespace EstimationPortal.Controllers
{
    [SessionTimeout]
    [NoDirectAccess]
    public class InqTypeMasterController : Controller
    {
        private DropDownCollection _dropDownCollection;

        private readonly SupplierDbContext _db;
        public InqTypeMasterController(SupplierDbContext db, DropDownCollection dropDownCollection)
        {
            _dropDownCollection = dropDownCollection;
            _db = db;
        }


        #region InqTypeMaster
        [HttpGet]
        public ActionResult Index()
        {
            var result = _db.Tbl_InqTypeMasters.OrderByDescending(s => s.Id).ToList();
            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tbl_InqTypeMasters mm)
        {
            if (ModelState.IsValid)
            {
                bool? dbdata = _dropDownCollection.CheckInqTypeExist(mm.InqType.Trim(), 0);
                if (dbdata == false)
                {
                    mm.IsDelete = false;
                    mm.CreatedDate = DateTime.Now;
                    mm.CreatedBy = Session["UserID"]?.ToString(); // Adjust based on your auth logic

                    _db.Tbl_InqTypeMasters.Add(mm);
                    _db.SaveChanges();

                    TempData["Success"] = "Inquiry Type created successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "This Inquiry Type already exists!");
                    return View(mm);
                }

            }

            return View(mm);
        }


        public ActionResult Edit(int id)
        {
            var user = _db.Tbl_InqTypeMasters.Find(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tbl_InqTypeMasters mm, int id)
        {
            if (ModelState.IsValid)
            {
                bool? dbdata = _dropDownCollection.CheckInqTypeExist(mm.InqType.Trim(), id);
                if (dbdata == false)
                {
                    var existingUser = _db.Tbl_InqTypeMasters.Find(mm.Id);
                    if (existingUser != null)
                    {
                        existingUser.InqType = mm.InqType.Trim();
                        existingUser.IsDelete = mm.IsDelete;
                        // Meta Data
                        existingUser.ModifiedDate = DateTime.Now;
                        existingUser.ModifiedBy = Session["UserID"]?.ToString();

                        _db.Entry(existingUser).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();

                        TempData["Success"] = "Inquiry Type updated successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        this.AddNotification("Inquiry not found!", "Error");
                        return View(mm);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "This Inquiry Type already exists!");
                    return View(mm);
                }

            }


            return View(mm);
        }
        #endregion
    }
}