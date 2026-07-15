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
    public class SalesMasterController : Controller
    {

        private readonly SupplierDbContext _db;
        public SalesMasterController(SupplierDbContext db)
        {
            _db = db;
        }


        #region SalesPersonMaster
        [HttpGet]
        public ActionResult Index()
        {
            var result = _db.Tbl_SalesPersonMasters.OrderByDescending(s => s.Id).ToList();
            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tbl_SalesPersonMasters user)
        {
            if (ModelState.IsValid)
            {
                // BaseModel properties
                user.IsDelete = false;
                user.CreatedDate = DateTime.Now;
                user.CreatedBy = Session["UserID"]?.ToString(); // Adjust based on your auth logic

                _db.Tbl_SalesPersonMasters.Add(user);
                _db.SaveChanges();

                TempData["Success"] = "Sales Person created successfully!";
                return RedirectToAction("Index");
            }

            return View(user);
        }


        public ActionResult Edit(int id)
        {
            var user = _db.Tbl_SalesPersonMasters.Find(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tbl_SalesPersonMasters user, int id)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _db.Tbl_SalesPersonMasters.Find(user.Id);
                if (existingUser != null)
                {
                    // Update fields
                    existingUser.SalesPerson = user.SalesPerson.Trim();
                    existingUser.IsDelete = user.IsDelete;
                    // Meta Data
                    existingUser.ModifiedDate = DateTime.Now;
                    existingUser.ModifiedBy = Session["UserID"]?.ToString();

                    _db.Entry(existingUser).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();

                    TempData["Success"] = "Sales Person updated successfully!";
                    return RedirectToAction("Index");
                }
            }

            return View(user);
        }
        #endregion
    }
}