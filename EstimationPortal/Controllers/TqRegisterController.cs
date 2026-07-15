using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.EMMA;
using EstimationPortal.CustomeAttribute;
using SUP_CORE.EFModel;
using SUP_DAL.EFContextProvider;

namespace EstimationPortal.Controllers
{
    [SessionTimeout]
    [NoDirectAccess]
    public class TqRegisterController : Controller
    {
        private DropDownCollection _dropDownCollection;

        private readonly SupplierDbContext _db;
        public TqRegisterController(SupplierDbContext db, DropDownCollection dropDownCollection)
        {
            _dropDownCollection = dropDownCollection;
            _db = db;
        }
        #region TQ Register
        [HttpGet]
        public ActionResult Index()
        {
            string financialYear = Session["FinancialYear"]?.ToString();
            var result = _db.Tbl_TqRegisters.Where(s=>s.FinancialYear == financialYear && s.IsDelete == false).OrderByDescending(s => s.Id).ToList();
            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    var InqMasterData = _db.Tbl_InquiryMasters.Where(s => s.Id.ToString() == item.SFNo.Trim() && s.IsDelete == false).FirstOrDefault();
                    if (InqMasterData != null)
                    {
                        item.Client = InqMasterData.Client != null ?  InqMasterData.Client.Trim() : "";
                        item.SFNo = InqMasterData.SFNo != null ? InqMasterData.SFNo.Trim() : "";
                        
                        item.TE = _db.Tbl_EngineerMasters.Where(s => s.Id.ToString() == InqMasterData.TE.Trim() && s.IsDelete == false).Select(s => s.EngineerName).FirstOrDefault();
                        item.PE = _db.Tbl_EngineerMasters.Where(s => s.Id.ToString() == InqMasterData.PE.Trim() && s.IsDelete == false).Select(s => s.EngineerName).FirstOrDefault();
                        item.EE = _db.Tbl_EngineerMasters.Where(s => s.Id.ToString() == InqMasterData.EE.Trim() && s.IsDelete == false).Select(s => s.EngineerName).FirstOrDefault();
                    }
                    
                    
                }
            }
            return View(result);
        }
        public ActionResult Create()
        {
            string financialYear = Session["FinancialYear"]?.ToString();
            ViewBag.SFNoList = _db.Tbl_InquiryMasters
                .Where(x => x.FinancialYear == financialYear && x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.SFNo ?? "").Trim()
                })
                .ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tbl_TqRegisters mm)
        {
            string financialYear = Session["FinancialYear"]?.ToString();
            if (ModelState.IsValid)
            {
                
                bool? dbdata = _dropDownCollection.CheckTqExist(mm.SFNo.Trim(),0);
                if (dbdata == false)
                {
                    // start file upload
                    if (mm.SelectedFile != null && mm.SelectedFile.ContentLength > 0)
                    {
                        string basePath = Server.MapPath("~/TqDocs/");
                        // Ensure base folder exists
                        if (!Directory.Exists(basePath))
                        {
                            Directory.CreateDirectory(basePath);
                        }
                        string ext = Path.GetExtension(mm.SelectedFile.FileName);
                        string uniqueFileName = Guid.NewGuid().ToString() + ext;

                        string filePath = Path.Combine(basePath, uniqueFileName);
                        mm.SelectedFile.SaveAs(filePath);

                        // OPTIONAL: save relative path to DB
                        mm.FilePath = filePath
                           .Replace(Server.MapPath("~/"), "/")
                           .Replace("\\", "/");
                        mm.UploadedFile = uniqueFileName;
                    }
                    // end file upload

                    mm.IsDelete = false;
                    mm.CreatedDate = DateTime.Now;
                    mm.CreatedBy = Session["UserID"]?.ToString(); // Adjust based on your auth logic
                    mm.FinancialYear = Session["FinancialYear"]?.ToString();
                    _db.Tbl_TqRegisters.Add(mm);
                    _db.SaveChanges();
                    if (mm.TqReceivedDate != null)
                    {
                        var InqMasterData = _db.Tbl_InquiryMasters.Where(s => s.Id.ToString() == mm.SFNo && s.IsDelete == false).FirstOrDefault();
                        if (InqMasterData != null)
                        {
                            InqMasterData.ActualSubDate = null;
                        }
                        _db.Entry(InqMasterData).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();
                    }
                    if (mm.TqDueDate != null) {
                        var InqMasterData = _db.Tbl_InquiryMasters.Where(s => s.Id.ToString() == mm.SFNo && s.IsDelete == false).FirstOrDefault();
                        if (InqMasterData != null)
                        {
                            InqMasterData.DueDate = mm.TqDueDate;
                        }
                        _db.Entry(InqMasterData).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();
                    }
                    if (mm.TqSubDate != null)
                    {
                        var InqMasterData = _db.Tbl_InquiryMasters.Where(s => s.Id.ToString() == mm.SFNo && s.IsDelete == false).FirstOrDefault();
                        if (InqMasterData != null)
                        {
                            InqMasterData.ActualSubDate = mm.TqSubDate;
                        }
                        _db.Entry(InqMasterData).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();
                    }
                    if (mm.TqSubDate == null)
                    {
                        var InqMasterData = _db.Tbl_InquiryMasters.Where(s => s.Id.ToString() == mm.SFNo && s.IsDelete == false).FirstOrDefault();
                        if (InqMasterData != null) 
                        {
                            // Update Enquiry Status
                            var EnqStatus = _db.Tbl_EnqStatusMasters
                                    .FirstOrDefault(s => s.EnqStatus == "TQ Stage" && s.IsDelete == false);

                            if (EnqStatus != null)
                            {
                                InqMasterData.EnqStatus = EnqStatus.Id.ToString();// TqStage Status
                            }
                            
                            _db.Entry(InqMasterData).State = System.Data.Entity.EntityState.Modified;
                            _db.SaveChanges();

                        }
                    }
                    else if (mm.TqSubDate != null)
                    {
                        var InqMasterData = _db.Tbl_InquiryMasters.Where(s => s.Id.ToString() == mm.SFNo && s.IsDelete == false).FirstOrDefault();
                        if (InqMasterData != null)
                        {
                            // Update Enquiry Status
                            var EnqStatus = _db.Tbl_EnqStatusMasters
                                    .FirstOrDefault(s => s.EnqStatus == "Completed" && s.IsDelete == false);

                            if (EnqStatus != null)
                            {
                                InqMasterData.EnqStatus = EnqStatus.Id.ToString();// Completed Status
                            }
                            _db.Entry(InqMasterData).State = System.Data.Entity.EntityState.Modified;
                            _db.SaveChanges();

                        }
                    }
                    TempData["Success"] = "Tq created successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    
                    ViewBag.SFNoList = _db.Tbl_InquiryMasters
               .Where(x => x.FinancialYear == financialYear && x.IsDelete == false)
               .Select(x => new SelectListItem
               {
                   Value = x.Id.ToString(),
                   Text = (x.SFNo ?? "").Trim()
               })
               .ToList();

                    ModelState.AddModelError("", "Tq already exist for this SF No. !");
                    return View(mm);
                }

            }
            else {
                ViewBag.SFNoList = _db.Tbl_InquiryMasters
               .Where(x => x.FinancialYear == financialYear && x.IsDelete == false)
               .Select(x => new SelectListItem
               {
                   Value = x.Id.ToString(),
                   Text = (x.SFNo ?? "").Trim()
               })
               .ToList();
                ModelState.AddModelError("", "Please fill required fields!");
                return View(mm);
            }

           // return View(mm);
        }
        public ActionResult Edit(int id)
        {
            string financialYear = Session["FinancialYear"]?.ToString();
            ViewBag.SFNoList = _db.Tbl_InquiryMasters
                .Where(x =>x.FinancialYear == financialYear && x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.SFNo ?? "").Trim()
                })
                .ToList();
            var mm = _db.Tbl_TqRegisters.Find(id);
            if (mm != null)
            {
                mm.Client = _db.Tbl_InquiryMasters
                            .Where(s => s.Id.ToString() == mm.SFNo.Trim() && s.IsDelete == false)
                            .Select(s => s.Client.Trim())
                            .FirstOrDefault() ?? "";
            }
            if (mm == null) return HttpNotFound();
            return View(mm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tbl_TqRegisters mm, int id)
        {
            string financialYear = Session["FinancialYear"]?.ToString();
            if (ModelState.IsValid)
            {
                bool? dbdata = _dropDownCollection.CheckTqExist(mm.SFNo.Trim(), id);
                if (dbdata == false)
                {
                    var existingUser = _db.Tbl_TqRegisters.Find(mm.Id);
                    if (mm.SelectedFile != null && mm.SelectedFile.ContentLength > 0)
                    {
                        string basePath = Server.MapPath("~/TqDocs/");

                        // 1. Ensure base folder exists
                        if (!Directory.Exists(basePath))
                        {
                            Directory.CreateDirectory(basePath);
                        }

                        // 2. DELETE OLD FILE FROM DISK
                        // Because you used GUIDs, ss.UploadedFile holds the exact unique name of the old file
                        if (!string.IsNullOrEmpty(existingUser.UploadedFile))
                        {
                            string oldFilePath = Path.Combine(basePath, existingUser.UploadedFile);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                try
                                {
                                    System.IO.File.Delete(oldFilePath);
                                }
                                catch (Exception ex)
                                {
                                    // Handle or log exception if the file is locked by another process
                                }
                            }
                        }

                        // 3. GENERATE UNIQUE NAME FOR THE NEW FILE
                        string ext = Path.GetExtension(mm.SelectedFile.FileName);
                        string uniqueFileName = Guid.NewGuid().ToString() + ext;
                        string filePath = Path.Combine(basePath, uniqueFileName);

                        // Save the new file to disk
                        mm.SelectedFile.SaveAs(filePath);

                        // 4. UPDATE THE DB ENTITY WITH NEW VALUES
                        existingUser.UploadedFile = uniqueFileName;
                        existingUser.FilePath = filePath
                            .Replace(Server.MapPath("~/"), "/")
                            .Replace("\\", "/");
                    }
                    if (existingUser != null)
                    {
                        // Update fields
                        existingUser.SFNo = mm.SFNo;
                        existingUser.TqNo = mm.TqNo;
                        existingUser.TqReceivedDate = mm.TqReceivedDate;
                        existingUser.TqDueDate = mm.TqDueDate;
                        existingUser.TqSubDate = mm.TqSubDate;
                        existingUser.IsDelete = mm.IsDelete;
                        existingUser.Remarks = mm.Remarks != null ? mm.Remarks.Trim() : "";
                        // Meta Data
                        existingUser.ModifiedDate = DateTime.Now;
                        existingUser.ModifiedBy = Session["UserID"]?.ToString();

                        _db.Entry(existingUser).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();
                        var InqMaster_Data = _db.Tbl_InquiryMasters.Where(s => s.Id.ToString() == mm.SFNo && s.IsDelete == false).FirstOrDefault();
                        if (InqMaster_Data != null)
                        {
                            InqMaster_Data.DueDate = mm.TqDueDate;
                            InqMaster_Data.ActualSubDate = mm.TqSubDate;
                        }
                        _db.Entry(InqMaster_Data).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();

                        if (mm.TqSubDate == null)
                        {
                            var InqMasterData = _db.Tbl_InquiryMasters.Where(s => s.Id.ToString() == mm.SFNo && s.IsDelete == false).FirstOrDefault();
                            if (InqMasterData != null)
                            {
                                // Update Enquiry Status
                                var EnqStatus = _db.Tbl_EnqStatusMasters
                                    .FirstOrDefault(s => s.EnqStatus == "TQ Stage" && s.IsDelete == false);

                                if (EnqStatus != null)
                                {
                                    InqMasterData.EnqStatus = EnqStatus.Id.ToString();// TqStage Status
                                }
                                
                              
                                _db.Entry(InqMasterData).State = System.Data.Entity.EntityState.Modified;
                                _db.SaveChanges();

                            }
                        }
                        else if (mm.TqSubDate != null)
                        {
                            var InqMasterData = _db.Tbl_InquiryMasters.Where(s => s.Id.ToString() == mm.SFNo && s.IsDelete == false).FirstOrDefault();
                            if (InqMasterData != null)
                            {
                                // Update Enquiry Status
                                var EnqStatus = _db.Tbl_EnqStatusMasters
                                    .FirstOrDefault(s => s.EnqStatus == "Completed" && s.IsDelete == false);

                                if (EnqStatus != null)
                                {
                                    InqMasterData.EnqStatus = EnqStatus.Id.ToString();// Completed Status
                                }

                                _db.Entry(InqMasterData).State = System.Data.Entity.EntityState.Modified;
                                _db.SaveChanges();

                            }
                        }

                        TempData["Success"] = "Tq updated successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.SFNoList = _db.Tbl_InquiryMasters
                       .Where(x => x.FinancialYear == financialYear && x.IsDelete == false)
                       .Select(x => new SelectListItem
                       {
                           Value = x.Id.ToString(),
                           Text = (x.SFNo ?? "").Trim()
                       })
                       .ToList();
                        ModelState.AddModelError("", "Tq not found!");
                        return View(mm);
                    }
                }
                else
                {
                    ViewBag.SFNoList = _db.Tbl_InquiryMasters
               .Where(x => x.FinancialYear == financialYear && x.IsDelete == false)
               .Select(x => new SelectListItem
               {
                   Value = x.Id.ToString(),
                   Text = (x.SFNo ?? "").Trim()
               })
               .ToList();

                    ModelState.AddModelError("", "Tq already exist for this SF No. !");
                    return View(mm);
                }

            }
            else
            {
                ViewBag.SFNoList = _db.Tbl_InquiryMasters
               .Where(x =>x.FinancialYear == financialYear && x.IsDelete == false)
               .Select(x => new SelectListItem
               {
                   Value = x.Id.ToString(),
                   Text = (x.SFNo ?? "").Trim()
               })
               .ToList();
                ModelState.AddModelError("", "Please fill required fields!");
                return View(mm);
            }
        }
        #endregion
        public JsonResult GetClient(string sfNo)
        {
            var client = _db.Tbl_InquiryMasters
                .Where(s => s.Id.ToString() == sfNo.Trim() && s.IsDelete == false)
                .Select(s => s.Client)
                .FirstOrDefault();

            return Json(client, JsonRequestBehavior.AllowGet);
        }
    }
}