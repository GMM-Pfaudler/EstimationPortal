using Castle.Core.Logging;
using SUP_BAL.IRepository;
using SUP_CORE.EFModel;
using SUP_CORE.EFModelViews;
using EstimationPortal.CustomeAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Office2010.Excel;
using SUP_CORE.DTO;
using SUP_DAL.EFContextProvider;
using System.Windows;
using EstimationPortal.Utility;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Controls.Primitives;
using System.Runtime.InteropServices;
using DocumentFormat.OpenXml.EMMA;
using Newtonsoft.Json;
using System.IO;
using EstimationPortal.CustomeAttribute;

namespace EstimationPortal.Controllers
{
    [SessionTimeout]
    [NoDirectAccess]
    public class UserMasterController : Controller
    {     
        private DropDownCollection _dropDownCollection;
       
        private readonly SupplierDbContext _db;
        public UserMasterController(SupplierDbContext db , DropDownCollection dropDownCollection)
        {
            _dropDownCollection = dropDownCollection;  
            _db = db;
        }


        #region UserMaster
        [HttpGet]
        public ActionResult Index()
        {
            var result = _db.Tbl_UserMasters.OrderByDescending(s => s.Id).ToList();
            return View(result);
        }

        // GET: UserMasters/Create
        public ActionResult Create()
        {
            // Populate Role Dropdown
            ViewBag.RoleId = new SelectList(_db.Tbl_Roles.Where(x => x.IsDelete == false), "Id", "RoleName");
            return View();
        }

        // POST: UserMasters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tbl_UserMasters user)
        {
            ViewBag.RoleId = new SelectList(_db.Tbl_Roles.Where(x => x.IsDelete == false), "Id", "RoleName", user.RoleId);
            if (ModelState.IsValid)
            {
                bool? dbdata = _dropDownCollection.CheckUserExist(user.UserID.Trim(), 0);
                if (dbdata == false)
                {
                    user.IsDelete = false;
                    user.CreatedDate = DateTime.Now;
                    user.CreatedBy = Session["UserID"]?.ToString(); // Adjust based on your auth logic

                    _db.Tbl_UserMasters.Add(user);
                    _db.SaveChanges();

                    TempData["Success"] = "User created successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    //this.AddNotification("This UserID Already Exists!", "Error");
                    ModelState.AddModelError("", "This UserID already exists!");
                    return View(user);
                }

            }

            // If validation fails, reload the dropdown
            
            return View(user);
        }


        // GET: UserMasters/Edit/5
        public ActionResult Edit(int id)
        {
            var user = _db.Tbl_UserMasters.Find(id);
            if (user == null) return HttpNotFound();

            ViewBag.RoleId = new SelectList(_db.Tbl_Roles.Where(x => x.IsDelete == false), "Id", "RoleName", user.RoleId);
            return View(user);
        }

        // POST: UserMasters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tbl_UserMasters user, int id)
        {
            ViewBag.RoleId = new SelectList(_db.Tbl_Roles.Where(x => x.IsDelete == false), "Id", "RoleName", user.RoleId);
            if (ModelState.IsValid)
            {
                bool? dbdata = _dropDownCollection.CheckUserExist(user.UserID.Trim(), id);
                if (dbdata == false)
                {
                    var existingUser = _db.Tbl_UserMasters.Find(user.Id);
                    if (existingUser != null)
                    {
                        // Update fields
                        existingUser.UserID = user.UserID;
                        existingUser.UserName = user.UserName;
                        existingUser.RoleId = user.RoleId;
                        existingUser.EmailID = user.EmailID;
                        existingUser.Password = user.Password;
                        existingUser.IsDelete = user.IsDelete;
                        // Meta Data
                        existingUser.ModifiedDate = DateTime.Now;
                        existingUser.ModifiedBy = Session["UserID"]?.ToString();

                        _db.Entry(existingUser).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();

                        TempData["Success"] = "User updated successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        this.AddNotification("User not found!", "Error");
                        return View(user);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "This UserID already exists!");
                    return View(user);
                }

            }

            
            return View(user);
        }
        #endregion


    }
}