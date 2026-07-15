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
    public class EquipTypeMasterController : Controller
    {
        private DropDownCollection _dropDownCollection;

        private readonly SupplierDbContext _db;
        public EquipTypeMasterController(SupplierDbContext db, DropDownCollection dropDownCollection)
        {
            _dropDownCollection = dropDownCollection;
            _db = db;
        }

        #region Equipment Type Master
        [HttpGet]
        public ActionResult Index()
        {
            var result = _db.Tbl_EquipTypeMasters.OrderByDescending(s => s.Id).ToList();
            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tbl_EquipTypeMasters mm)
        {
            if (ModelState.IsValid)
            {
                bool? dbdata = _dropDownCollection.CheckEquipTypeExist(mm.EquipmentType.Trim(), 0);
                if (dbdata == false)
                {
                    mm.IsDelete = false;
                    mm.CreatedDate = DateTime.Now;
                    mm.CreatedBy = Session["UserID"]?.ToString(); // Adjust based on your auth logic

                    _db.Tbl_EquipTypeMasters.Add(mm);
                    _db.SaveChanges();

                    TempData["Success"] = "Equipment Type created successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "This Equipment Type already exists!");
                    return View(mm);
                }

            }

            return View(mm);
        }


        public ActionResult Edit(int id)
        {
            var user = _db.Tbl_EquipTypeMasters.Find(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tbl_EquipTypeMasters mm, int id)
        {
            if (ModelState.IsValid)
            {
                bool? dbdata = _dropDownCollection.CheckEquipTypeExist(mm.EquipmentType.Trim(), id);
                if (dbdata == false)
                {
                    var existingUser = _db.Tbl_EquipTypeMasters.Find(mm.Id);
                    if (existingUser != null)
                    {
                        existingUser.EquipmentType = mm.EquipmentType.Trim();
                        existingUser.IsDelete = mm.IsDelete;
                        // Meta Data
                        existingUser.ModifiedDate = DateTime.Now;
                        existingUser.ModifiedBy = Session["UserID"]?.ToString();

                        _db.Entry(existingUser).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();

                        TempData["Success"] = "Equipment Type updated successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        this.AddNotification("Equipment Type not found!", "Error");
                        return View(mm);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "This Equipment Type already exists!");
                    return View(mm);
                }

            }


            return View(mm);
        }
        #endregion
    }
}