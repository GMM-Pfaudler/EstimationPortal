using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using EstimationPortal.CustomeAttribute;
using EstimationPortal.Utility;
using SUP_CORE.EFModel;
using SUP_CORE.EFModelViews;
using SUP_DAL.EFContextProvider;
using static System.Web.Razor.Parser.SyntaxConstants;

namespace EstimationPortal.Controllers
{
    [SessionTimeout]
    [NoDirectAccess]
    public class InquiryMasterController : Controller
    {
        private DropDownCollection _dropDownCollection;

        private readonly SupplierDbContext _db;
        public InquiryMasterController(SupplierDbContext db, DropDownCollection dropDownCollection)
        {
            _dropDownCollection = dropDownCollection;
            _db = db;
        }
        #region InquiryMaster
        [HttpGet]
        public ActionResult Index()
        {
            string financialYear = Session["FinancialYear"]?.ToString();
            List<InqListViewModel> InquiryList = _dropDownCollection.GetInquiryList(financialYear);
            return View(InquiryList);
            
        }
        public ActionResult Create()
        {
            ViewBag.SalesPersonList = _db.Tbl_SalesPersonMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.SalesPerson ?? "").Trim()
                })
                .ToList();
            ViewBag.EnqStatusList = _db.Tbl_EnqStatusMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EnqStatus ?? "").Trim()
                })
                .ToList();
            ViewBag.InqTypeList = _db.Tbl_InqTypeMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.InqType ?? "").Trim()
                })
                .ToList();
            ViewBag.SubTypeList = _db.Tbl_SubTypeMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.SubType ?? "").Trim()
                })
                .ToList();
            ViewBag.EquipTypeList = _db.Tbl_EquipTypeMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EquipmentType ?? "").Trim()
                })
                .ToList();
           
            ViewBag.TEList = _db.Tbl_EngineerMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EngineerName ?? "").Trim()
                })
                .ToList();
            ViewBag.PEList = _db.Tbl_EngineerMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EngineerName ?? "").Trim()
                })
                .ToList();

            ViewBag.EEList = _db.Tbl_EngineerMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EngineerName ?? "").Trim()
                })
                .ToList();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tbl_InquiryMasters mm)
        {
            if (ModelState.IsValid)
            {
                string savedFilePathForEmail = null;
                // 1. Process File Upload First
                if (mm.SelectedFile != null && mm.SelectedFile.ContentLength > 0)
                {
                    string basePath = Server.MapPath("~/Docs/");
                    if (!Directory.Exists(basePath))
                    {
                        Directory.CreateDirectory(basePath);
                    }
                    string ext = Path.GetExtension(mm.SelectedFile.FileName);
                    string uniqueFileName = Guid.NewGuid().ToString() + ext;

                    // This is the absolute disk path we need for the mail attachment
                    savedFilePathForEmail = Path.Combine(basePath, uniqueFileName);
                    mm.SelectedFile.SaveAs(savedFilePathForEmail);

                    // Save relative path to DB
                    mm.FilePath = savedFilePathForEmail
                        .Replace(Server.MapPath("~/"), "/")
                        .Replace("\\", "/");
                    mm.UploadedFile = uniqueFileName;
                }

                // 2. Email Trigger Logic
                if (mm.IsMailTrigger == true)
                {
                    string SFNO = mm.SFNo != null ? mm.SFNo.Trim() : "";
                    string SalesPerson = _db.Tbl_SalesPersonMasters
                                .FirstOrDefault(s => s.Id.ToString() == mm.SalesPerson && s.IsDelete == false)?
                                .SalesPerson?.Trim() ?? "";
                    string InqReceiptDate = mm.InqReceiptDate?.ToString("dd-MM-yyyy") ?? "";
                    string InqType = _db.Tbl_InqTypeMasters
                            .FirstOrDefault(s => s.Id.ToString() == mm.InqType && s.IsDelete == false)?
                            .InqType?.Trim() ?? "";
                    string DomExp = mm.DomExp != null ? mm.DomExp.Trim() : "";
                    string DueDate= mm.DueDate?.ToString("dd-MM-yyyy") ?? "";
                    string SubType = _db.Tbl_SubTypeMasters
                            .FirstOrDefault(s => s.Id.ToString() == mm.SubType.ToString() && s.IsDelete == false)?
                            .SubType?.Trim() ?? "";
                    string EnqStatus = _db.Tbl_EnqStatusMasters
                                .FirstOrDefault(s => s.Id.ToString() == mm.EnqStatus && s.IsDelete == false)?
                                .EnqStatus?.Trim() ?? "";
                    string Client = mm.Client != null ? mm.Client.Trim() : "";
                    string EPC = mm.EPC != null ? mm.EPC.Trim() : "";
                    string EquipType = _db.Tbl_EquipTypeMasters
                                    .FirstOrDefault(s => s.Id.ToString() == mm.EquipType.Trim() && s.IsDelete == false)?
                                    .EquipmentType?.Trim() ?? "";
                    string TagQty = mm.TagQty.ToString();
                    string TotalQty = mm.TotalQty.ToString();
                    string DesignScope = mm.DesignScope != null ? mm.DesignScope.Trim() : "";
                    string TEname = _db.Tbl_EngineerMasters.Where(x => x.Id.ToString() == mm.TE && x.IsDelete == false).Select(x => x.EngineerName).FirstOrDefault();
                    string PEname = _db.Tbl_EngineerMasters.Where(x => x.Id.ToString() == mm.PE && x.IsDelete == false).Select(x => x.EngineerName).FirstOrDefault();
                    string EEname = _db.Tbl_EngineerMasters.Where(x => x.Id.ToString() == mm.EE && x.IsDelete == false).Select(x => x.EngineerName).FirstOrDefault();
                    string Remarks = mm.Remarks != null ? mm.Remarks.Trim() : "";

                    if (!string.IsNullOrEmpty(mm.TE) || !string.IsNullOrEmpty(mm.PE) || !string.IsNullOrEmpty(mm.EE))
                    {
                        string subject = "Notification : New Enquiry Assigned // SF No. " + SFNO;
                        
                        StringBuilder sb = new StringBuilder();

                        sb.Append("<html><head>");

                        sb.Append("<style>");
                        sb.Append("body { margin:20px;font-family:Arial;font-size:14px; }");
                        sb.Append("table { border-collapse:collapse;width:100%;margin-top:10px; }");
                        sb.Append("th { background:#2F75B5;color:white;padding:6px;border:1px solid #d9d9d9; }");
                        sb.Append("td { padding:6px;border:1px solid #d9d9d9;text-align:center; }");
                        sb.Append("</style>");

                        sb.Append("</head><body>");

                        sb.Append("<p>Dear User,</p>");

                        sb.Append("<p>Greetings of the day..!</p>");

                        sb.Append("<p>The enquiries listed below have been assigned to you. Please refer to the enquiry status and take the necessary actions accordingly.</p>");

                        // ===================== OVERDUE TABLE =====================
                        sb.Append("<table>");

                        sb.Append("<tr>");
                        sb.Append("<th>SF NO</th>");
                        sb.Append("<th>SALES PERSON</th>");
                        sb.Append("<th>INQUIRY RECEIPT DATE</th>");
                        sb.Append("<th>INQUIRY TYPE</th>");
                        sb.Append("<th>DOM / EXP</th>");
                        sb.Append("<th>DUE DATE</th>");
                        sb.Append("<th>SUB TYPE</th>");
                        sb.Append("<th>ENQUIRY STATUS</th>");
                        sb.Append("<th>End User / CLIENT</th>");
                        sb.Append("<th>EPC / Licensor</th>");
                        sb.Append("<th>EQUIPMENT TYPE</th>");
                        sb.Append("<th>TAG QTY.</th>");
                        sb.Append("<th>TOTAL QTY.</th>");
                        sb.Append("<th>DESIGN SCOPE</th>");
                        sb.Append("<th>TE</th>");
                        sb.Append("<th>PE</th>");
                        sb.Append("<th>EE</th>");
                        sb.Append("<th>REMARKS</th>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append($"<td>{SFNO}</td>");
                        sb.Append($"<td>{SalesPerson}</td>");
                        sb.Append($"<td>{InqReceiptDate}</td>");
                        sb.Append($"<td>{InqType}</td>");
                        sb.Append($"<td>{DomExp}</td>");
                        sb.Append($"<td>{DueDate}</td>");
                        sb.Append($"<td>{SubType}</td>");
                        sb.Append($"<td>{EnqStatus}</td>");
                        sb.Append($"<td>{Client}</td>");
                        sb.Append($"<td>{EPC}</td>");
                        sb.Append($"<td>{EquipType}</td>");
                        sb.Append($"<td>{TagQty}</td>");
                        sb.Append($"<td>{TotalQty}</td>");
                        sb.Append($"<td>{DesignScope}</td>");
                        sb.Append($"<td>{TEname}</td>");
                        sb.Append($"<td>{PEname}</td>");
                        sb.Append($"<td>{EEname}</td>");
                        sb.Append($"<td>{Remarks}</td>");

                        sb.Append("</tr>");

                        sb.Append("</table>");
                        // Mail Notes start
                        if (!string.IsNullOrWhiteSpace(mm.MailNotes))
                        {
                            sb.Append("<div style='margin-top:20px;'>");
                            sb.Append($"<b>Notes :</b> {mm.MailNotes.Replace(Environment.NewLine, "<br/>")}");
                            sb.Append("</div>");
                        }
                        // Mail Notes end
                        sb.Append("<p style='margin-top:20px; margin-bottom:5px; color: #444444; font-size: 14px; line-height:1.4;'>");
                        sb.Append("Best Regards,<br/>");
                        sb.Append("<span style='font-size: 16px; font-weight: bold; color: #000080;'>GMM Pfaudler LTD</span>");
                        sb.Append("</p>");

                        sb.Append("<i>This is a system generated email, please do not respond.</i>");

                        sb.Append("</body></html>");


                        string emailBody = sb.ToString();
                        var ccEmails = _db.Tbl_EmailMasters
                                    .Where(x => x.IsDelete == false
                                             && x.Email_ID != null
                                             && x.Email_ID.Trim() != "")
                                    .Select(x => x.Email_ID.Trim())
                                    .ToList();
                        string TE_Email = null; string PE_Email = null; string EE_Email = null;

                        if (!string.IsNullOrEmpty(mm.TE))
                        {
                            var teEmail = _db.Tbl_EngineerMasters.Where(x => x.Id.ToString() == mm.TE && x.IsDelete == false).Select(x => x.EnggEmail).FirstOrDefault();
                            if (!string.IsNullOrEmpty(teEmail))
                            {
                                TE_Email = teEmail.Trim();

                            }
                        }
                        if (!string.IsNullOrEmpty(mm.PE))
                        {
                            var peEmail = _db.Tbl_EngineerMasters.Where(x => x.Id.ToString() == mm.PE && x.IsDelete == false).Select(x => x.EnggEmail).FirstOrDefault();
                            if (!string.IsNullOrEmpty(peEmail))
                            {
                                PE_Email = peEmail.Trim();
                            }
                        }
                        if (!string.IsNullOrEmpty(mm.EE))
                        {
                            var eeEmail = _db.Tbl_EngineerMasters.Where(x => x.Id.ToString() == mm.EE && x.IsDelete == false).Select(x => x.EnggEmail).FirstOrDefault();
                            if (!string.IsNullOrEmpty(eeEmail))
                            {
                                EE_Email = eeEmail.Trim();
                                //EmailService.SendEmailInquiryCreated(eeEmail, subject, emailBody, savedFilePathForEmail, ccEmails);
                            }
                        }
                        if (!string.IsNullOrEmpty(TE_Email) || !string.IsNullOrEmpty(PE_Email) || !string.IsNullOrEmpty(EE_Email))
                        {
                            EmailService.SendEmailInquiryCreated(subject, emailBody, savedFilePathForEmail, ccEmails, TE_Email, PE_Email, EE_Email);
                        }
                    }
                }
                mm.IsDelete = false;
                mm.CreatedDate = DateTime.Now;
                mm.CreatedBy = Session["UserID"]?.ToString(); // Adjust based on your auth logic
                mm.FinancialYear = Session["FinancialYear"]?.ToString(); 
                _db.Tbl_InquiryMasters.Add(mm);
                _db.SaveChanges();

                TempData["Success"] = "Inquiry created successfully!";
                return RedirectToAction("Index");
                

            }
            else
            {
                ViewBag.SalesPersonList = _db.Tbl_SalesPersonMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.SalesPerson ?? "").Trim()
                })
                .ToList();
                ViewBag.EnqStatusList = _db.Tbl_EnqStatusMasters
               .Where(x => x.IsDelete == false)
               .Select(x => new SelectListItem
               {
                   Value = x.Id.ToString(),
                   Text = (x.EnqStatus ?? "").Trim()
               })
               .ToList();
                ViewBag.InqTypeList = _db.Tbl_InqTypeMasters
                    .Where(x => x.IsDelete == false)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = (x.InqType ?? "").Trim()
                    })
                    .ToList();
                ViewBag.SubTypeList = _db.Tbl_SubTypeMasters
                    .Where(x => x.IsDelete == false)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = (x.SubType ?? "").Trim()
                    })
                    .ToList();
                ViewBag.EquipTypeList = _db.Tbl_EquipTypeMasters
                    .Where(x => x.IsDelete == false)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = (x.EquipmentType ?? "").Trim()
                    })
                    .ToList();

                ViewBag.TEList = _db.Tbl_EngineerMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EngineerName ?? "").Trim()
                })
                .ToList();
                ViewBag.PEList = _db.Tbl_EngineerMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EngineerName ?? "").Trim()
                })
                .ToList();

                ViewBag.EEList = _db.Tbl_EngineerMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EngineerName ?? "").Trim()
                })
                .ToList();

                ModelState.AddModelError("", "Please fill required fields!");
                return View(mm);
            }

        }
        public ActionResult Edit(int id)
        {
            ViewBag.SalesPersonList = _db.Tbl_SalesPersonMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.SalesPerson ?? "").Trim()
                })
                .ToList();
            ViewBag.EnqStatusList = _db.Tbl_EnqStatusMasters
               .Where(x => x.IsDelete == false)
               .Select(x => new SelectListItem
               {
                   Value = x.Id.ToString(),
                   Text = (x.EnqStatus ?? "").Trim()
               })
               .ToList();
            ViewBag.InqTypeList = _db.Tbl_InqTypeMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.InqType ?? "").Trim()
                })
                .ToList();
            ViewBag.SubTypeList = _db.Tbl_SubTypeMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.SubType ?? "").Trim()
                })
                .ToList();
            ViewBag.EquipTypeList = _db.Tbl_EquipTypeMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EquipmentType ?? "").Trim()
                })
                .ToList();

            ViewBag.TEList = _db.Tbl_EngineerMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EngineerName ?? "").Trim()
                })
                .ToList();
            ViewBag.PEList = _db.Tbl_EngineerMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EngineerName ?? "").Trim()
                })
                .ToList();

            ViewBag.EEList = _db.Tbl_EngineerMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EngineerName ?? "").Trim()
                })
                .ToList();
            var mm = _db.Tbl_InquiryMasters.Find(id);
            if (mm == null) return HttpNotFound();
            return View(mm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tbl_InquiryMasters mm, int id)
        {
            if (ModelState.IsValid)
            {
                var ss = _db.Tbl_InquiryMasters.Find(mm.Id);
                if (ss == null)
                {
                    return HttpNotFound();
                }
                string basePath = Server.MapPath("~/Docs/");
                // Track the absolute physical file path to pass to the email attachment
                string attachmentPathForEmail = null;

                // 1. PROCESS FILE UPLOAD FIRST (If a new file is provided)
                if (mm.SelectedFile != null && mm.SelectedFile.ContentLength > 0)
                {
                    // Ensure base folder exists
                    if (!Directory.Exists(basePath))
                    {
                        Directory.CreateDirectory(basePath);
                    }

                    // DELETE OLD PHYSICAL FILE FROM DISK
                    if (!string.IsNullOrEmpty(ss.UploadedFile))
                    {
                        string oldFilePath = Path.Combine(basePath, ss.UploadedFile);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            try
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                            catch (Exception ex)
                            {
                                // Handle or log exception if the file is locked
                            }
                        }
                    }

                    // GENERATE UNIQUE NAME FOR THE NEW FILE
                    string ext = Path.GetExtension(mm.SelectedFile.FileName);
                    string uniqueFileName = Guid.NewGuid().ToString() + ext;
                    attachmentPathForEmail = Path.Combine(basePath, uniqueFileName);

                    // Save the new file to disk
                    mm.SelectedFile.SaveAs(attachmentPathForEmail);

                    // Update database entity properties with the brand new file info
                    ss.UploadedFile = uniqueFileName;
                    ss.FilePath = attachmentPathForEmail
                        .Replace(Server.MapPath("~/"), "/")
                        .Replace("\\", "/");
                }
                else
                {
                    // FALLBACK: If no new file was uploaded, check if an existing file is already present
                    if (!string.IsNullOrEmpty(ss.UploadedFile))
                    {
                        attachmentPathForEmail = Path.Combine(basePath, ss.UploadedFile);
                    }
                }

                // 2. EMAIL TRIGGER LOGIC (Same as Create)
                if (mm.IsMailTrigger == true)
                {
                    string SFNO = mm.SFNo != null ? mm.SFNo.Trim() : "";
                    string SalesPerson = _db.Tbl_SalesPersonMasters
                                .FirstOrDefault(s => s.Id.ToString() == mm.SalesPerson && s.IsDelete == false)?
                                .SalesPerson?.Trim() ?? "";
                    string InqReceiptDate = mm.InqReceiptDate?.ToString("dd-MM-yyyy") ?? "";
                    string InqType = _db.Tbl_InqTypeMasters
                            .FirstOrDefault(s => s.Id.ToString() == mm.InqType && s.IsDelete == false)?
                            .InqType?.Trim() ?? "";
                    string DomExp = mm.DomExp != null ? mm.DomExp.Trim() : "";
                    string DueDate = mm.DueDate?.ToString("dd-MM-yyyy") ?? "";
                    string SubType = _db.Tbl_SubTypeMasters
                            .FirstOrDefault(s => s.Id.ToString() == mm.SubType.ToString() && s.IsDelete == false)?
                            .SubType?.Trim() ?? "";
                    string EnqStatus = _db.Tbl_EnqStatusMasters
                                .FirstOrDefault(s => s.Id.ToString() == mm.EnqStatus && s.IsDelete == false)?
                                .EnqStatus?.Trim() ?? "";
                    string Client = mm.Client != null ? mm.Client.Trim() : "";
                    string EPC = mm.EPC != null ? mm.EPC.Trim() : "";
                    string EquipType = _db.Tbl_EquipTypeMasters
                                    .FirstOrDefault(s => s.Id.ToString() == mm.EquipType.Trim() && s.IsDelete == false)?
                                    .EquipmentType?.Trim() ?? "";
                    string TagQty = mm.TagQty.ToString();
                    string TotalQty = mm.TotalQty.ToString();
                    string DesignScope = mm.DesignScope != null ? mm.DesignScope.Trim() : "";
                    string TEname = _db.Tbl_EngineerMasters.Where(x => x.Id.ToString() == mm.TE && x.IsDelete == false).Select(x => x.EngineerName).FirstOrDefault();
                    string PEname = _db.Tbl_EngineerMasters.Where(x => x.Id.ToString() == mm.PE && x.IsDelete == false).Select(x => x.EngineerName).FirstOrDefault();
                    string EEname = _db.Tbl_EngineerMasters.Where(x => x.Id.ToString() == mm.EE && x.IsDelete == false).Select(x => x.EngineerName).FirstOrDefault();
                    string Remarks = mm.Remarks != null ? mm.Remarks.Trim() : "";

                    if (!string.IsNullOrEmpty(mm.TE) || !string.IsNullOrEmpty(mm.PE) || !string.IsNullOrEmpty(mm.EE))
                    {
                        string subject = "Notification : Enquiry Updated // SF No. " + SFNO;

                        StringBuilder sb = new StringBuilder();

                        sb.Append("<html><head>");

                        sb.Append("<style>");
                        sb.Append("body { margin:20px;font-family:Arial;font-size:14px; }");
                        sb.Append("table { border-collapse:collapse;width:100%;margin-top:10px; }");
                        sb.Append("th { background:#2F75B5;color:white;padding:6px;border:1px solid #d9d9d9; }");
                        sb.Append("td { padding:6px;border:1px solid #d9d9d9;text-align:center; }");
                        sb.Append("</style>");

                        sb.Append("</head><body>");

                        sb.Append("<p>Dear User,</p>");

                        sb.Append("<p>Greetings of the day..!</p>");

                        sb.Append("<p>The enquiries listed below has been updated. Please refer to the enquiry status and take the necessary actions accordingly.</p>");

                        // ===================== OVERDUE TABLE =====================
                        sb.Append("<table>");

                        sb.Append("<tr>");
                        sb.Append("<th>SF NO</th>");
                        sb.Append("<th>SALES PERSON</th>");
                        sb.Append("<th>INQUIRY RECEIPT DATE</th>");
                        sb.Append("<th>INQUIRY TYPE</th>");
                        sb.Append("<th>DOM / EXP</th>");
                        sb.Append("<th>DUE DATE</th>");
                        sb.Append("<th>SUB TYPE</th>");
                        sb.Append("<th>ENQUIRY STATUS</th>");
                        sb.Append("<th>End User / CLIENT</th>");
                        sb.Append("<th>EPC / Licensor</th>");
                        sb.Append("<th>EQUIPMENT TYPE</th>");
                        sb.Append("<th>TAG QTY.</th>");
                        sb.Append("<th>TOTAL QTY.</th>");
                        sb.Append("<th>DESIGN SCOPE</th>");
                        sb.Append("<th>TE</th>");
                        sb.Append("<th>PE</th>");
                        sb.Append("<th>EE</th>");
                        sb.Append("<th>REMARKS</th>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append($"<td>{SFNO}</td>");
                        sb.Append($"<td>{SalesPerson}</td>");
                        sb.Append($"<td>{InqReceiptDate}</td>");
                        sb.Append($"<td>{InqType}</td>");
                        sb.Append($"<td>{DomExp}</td>");
                        sb.Append($"<td>{DueDate}</td>");
                        sb.Append($"<td>{SubType}</td>");
                        sb.Append($"<td>{EnqStatus}</td>");
                        sb.Append($"<td>{Client}</td>");
                        sb.Append($"<td>{EPC}</td>");
                        sb.Append($"<td>{EquipType}</td>");
                        sb.Append($"<td>{TagQty}</td>");
                        sb.Append($"<td>{TotalQty}</td>");
                        sb.Append($"<td>{DesignScope}</td>");
                        sb.Append($"<td>{TEname}</td>");
                        sb.Append($"<td>{PEname}</td>");
                        sb.Append($"<td>{EEname}</td>");
                        sb.Append($"<td>{Remarks}</td>");

                        sb.Append("</tr>");

                        sb.Append("</table>");
                        // Mail Notes start
                        if (!string.IsNullOrWhiteSpace(mm.MailNotes))
                        {
                            sb.Append("<div style='margin-top:20px;'>");
                            sb.Append($"<b>Notes :</b> {mm.MailNotes.Replace(Environment.NewLine, "<br/>")}");
                            sb.Append("</div>");
                        }
                        // Mail Notes end
                        sb.Append("<p style='margin-top:20px; margin-bottom:5px; color: #444444; font-size: 14px; line-height:1.4;'>");
                        sb.Append("Best Regards,<br/>");
                        sb.Append("<span style='font-size: 16px; font-weight: bold; color: #000080;'>GMM Pfaudler LTD</span>");
                        sb.Append("</p>");

                        sb.Append("<i>This is a system generated email, please do not respond.</i>");

                        sb.Append("</body></html>");


                        string emailBody = sb.ToString();

                        var ccEmails = _db.Tbl_EmailMasters
                                    .Where(x => x.IsDelete == false
                                             && x.Email_ID != null
                                             && x.Email_ID.Trim() != "")
                                    .Select(x => x.Email_ID.Trim())
                                    .ToList();
                        string TE_Email = null; string PE_Email = null; string EE_Email = null;
                        if (!string.IsNullOrEmpty(mm.TE))
                        {
                            var teEmail = _db.Tbl_EngineerMasters.Where(x => x.Id.ToString() == mm.TE && x.IsDelete == false).Select(x => x.EnggEmail).FirstOrDefault();
                            if (!string.IsNullOrEmpty(teEmail))
                            {
                                TE_Email = teEmail.Trim();
                            }
                        }
                        if (!string.IsNullOrEmpty(mm.PE))
                        {
                            var peEmail = _db.Tbl_EngineerMasters.Where(x => x.Id.ToString() == mm.PE && x.IsDelete == false).Select(x => x.EnggEmail).FirstOrDefault();
                            if (!string.IsNullOrEmpty(peEmail))
                            {
                                PE_Email = peEmail.Trim();
                            }
                        }
                        if (!string.IsNullOrEmpty(mm.EE))
                        {
                            var eeEmail = _db.Tbl_EngineerMasters.Where(x => x.Id.ToString() == mm.EE && x.IsDelete == false).Select(x => x.EnggEmail).FirstOrDefault();
                            if (!string.IsNullOrEmpty(eeEmail))
                            {
                                EE_Email = eeEmail.Trim();
                            }
                        }
                        if (!string.IsNullOrEmpty(TE_Email) || !string.IsNullOrEmpty(PE_Email) || !string.IsNullOrEmpty(EE_Email))
                        {
                            EmailService.SendEmailInquiryCreated(subject, emailBody, attachmentPathForEmail, ccEmails, TE_Email, PE_Email, EE_Email);
                        }


                    }
                }
                //if (mm.SelectedFile != null && mm.SelectedFile.ContentLength > 0)
                //{
                //    string basePath = Server.MapPath("~/Docs/");

                //    // 1. Ensure base folder exists
                //    if (!Directory.Exists(basePath))
                //    {
                //        Directory.CreateDirectory(basePath);
                //    }

                //    // 2. DELETE OLD FILE FROM DISK
                //    // Because you used GUIDs, ss.UploadedFile holds the exact unique name of the old file
                //    if (!string.IsNullOrEmpty(ss.UploadedFile))
                //    {
                //        string oldFilePath = Path.Combine(basePath, ss.UploadedFile);
                //        if (System.IO.File.Exists(oldFilePath))
                //        {
                //            try
                //            {
                //                System.IO.File.Delete(oldFilePath);
                //            }
                //            catch (Exception ex)
                //            {
                //                // Handle or log exception if the file is locked by another process
                //            }
                //        }
                //    }

                //    // 3. GENERATE UNIQUE NAME FOR THE NEW FILE
                //    string ext = Path.GetExtension(mm.SelectedFile.FileName);
                //    string uniqueFileName = Guid.NewGuid().ToString() + ext;
                //    string filePath = Path.Combine(basePath, uniqueFileName);

                //    // Save the new file to disk
                //    mm.SelectedFile.SaveAs(filePath);

                //    // 4. UPDATE THE DB ENTITY WITH NEW VALUES
                //    ss.UploadedFile = uniqueFileName;
                //    ss.FilePath = filePath
                //        .Replace(Server.MapPath("~/"), "/")
                //        .Replace("\\", "/");
                //}
                if (ss != null)
                {
                    // Update fields
                    ss.SFNo = mm.SFNo.Trim();
                    ss.SalesPerson = mm.SalesPerson;
                    ss.InqReceiptDate = mm.InqReceiptDate;
                    ss.InqType = mm.InqType;
                    ss.DomExp = mm.DomExp;
                    ss.DueDate = mm.DueDate;
                    ss.ActualSubDate = mm.ActualSubDate;
                    ss.SubType = mm.SubType;
                    ss.EnqStatus = mm.EnqStatus;
                    ss.Client=mm.Client;
                    ss.EPC = mm.EPC;
                    ss.EquipType=mm.EquipType;
                    ss.TagQty = mm.TagQty;
                    ss.TotalQty = mm.TotalQty;
                    ss.MajorMOC = mm.MajorMOC;
                    ss.DesignScope = mm.DesignScope;
                    ss.TE = mm.TE;
                    ss.PE = mm.PE;
                    ss.EE = mm.EE;
                    ss.ProjectNo = mm.ProjectNo;
                    ss.ProjReceivedDate = mm.ProjReceivedDate;
                    ss.ProjStatus = mm.ProjStatus;
                    ss.RsValue = mm.RsValue;
                    ss.Remarks = mm.Remarks;

                    
                    
                    // Meta Data
                    ss.ModifiedDate = DateTime.Now;
                    ss.ModifiedBy = Session["UserID"]?.ToString();

                    _db.Entry(ss).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();

                    TempData["Success"] = "Inquiry updated successfully!";
                    return RedirectToAction("Index");
                }
                
            }
            
                ViewBag.SalesPersonList = _db.Tbl_SalesPersonMasters
               .Where(x => x.IsDelete == false)
               .Select(x => new SelectListItem
               {
                   Value = x.Id.ToString(),
                   Text = (x.SalesPerson ?? "").Trim()
               })
               .ToList();
            ViewBag.EnqStatusList = _db.Tbl_EnqStatusMasters
               .Where(x => x.IsDelete == false)
               .Select(x => new SelectListItem
               {
                   Value = x.Id.ToString(),
                   Text = (x.EnqStatus ?? "").Trim()
               })
               .ToList();
            ViewBag.InqTypeList = _db.Tbl_InqTypeMasters
                    .Where(x => x.IsDelete == false)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = (x.InqType ?? "").Trim()
                    })
                    .ToList();
                ViewBag.SubTypeList = _db.Tbl_SubTypeMasters
                    .Where(x => x.IsDelete == false)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = (x.SubType ?? "").Trim()
                    })
                    .ToList();
                ViewBag.EquipTypeList = _db.Tbl_EquipTypeMasters
                    .Where(x => x.IsDelete == false)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = (x.EquipmentType ?? "").Trim()
                    })
                    .ToList();

                ViewBag.TEList = _db.Tbl_EngineerMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EngineerName ?? "").Trim()
                })
                .ToList();
            ViewBag.PEList = _db.Tbl_EngineerMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EngineerName ?? "").Trim()
                })
                .ToList();

            ViewBag.EEList = _db.Tbl_EngineerMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EngineerName ?? "").Trim()
                })
                .ToList();
            return View(mm);
            

                
        }

        public ActionResult Copy(int id)
        {
            ViewBag.SalesPersonList = _db.Tbl_SalesPersonMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.SalesPerson ?? "").Trim()
                })
                .ToList();
            ViewBag.EnqStatusList = _db.Tbl_EnqStatusMasters
               .Where(x => x.IsDelete == false)
               .Select(x => new SelectListItem
               {
                   Value = x.Id.ToString(),
                   Text = (x.EnqStatus ?? "").Trim()
               })
               .ToList();
            ViewBag.InqTypeList = _db.Tbl_InqTypeMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.InqType ?? "").Trim()
                })
                .ToList();
            ViewBag.SubTypeList = _db.Tbl_SubTypeMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.SubType ?? "").Trim()
                })
                .ToList();
            ViewBag.EquipTypeList = _db.Tbl_EquipTypeMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EquipmentType ?? "").Trim()
                })
                .ToList();

            ViewBag.TEList = _db.Tbl_EngineerMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EngineerName ?? "").Trim()
                })
                .ToList();
            ViewBag.PEList = _db.Tbl_EngineerMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EngineerName ?? "").Trim()
                })
                .ToList();

            ViewBag.EEList = _db.Tbl_EngineerMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EngineerName ?? "").Trim()
                })
                .ToList();
            var mm = _db.Tbl_InquiryMasters.Find(id);
            if (mm == null) return HttpNotFound();
            return View(mm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Copy(Tbl_InquiryMasters mm, int id)
        {
            if (ModelState.IsValid)
            {
                mm.IsDelete = false;
                mm.CreatedDate = DateTime.Now;
                mm.CreatedBy = Session["UserID"]?.ToString(); // Adjust based on your auth logic
                mm.FinancialYear = Session["FinancialYear"]?.ToString();
                _db.Tbl_InquiryMasters.Add(mm);
                _db.SaveChanges();

                TempData["Success"] = "Inquiry created successfully!";
                return RedirectToAction("Index");

            }

            ViewBag.SalesPersonList = _db.Tbl_SalesPersonMasters
           .Where(x => x.IsDelete == false)
           .Select(x => new SelectListItem
           {
               Value = x.Id.ToString(),
               Text = (x.SalesPerson ?? "").Trim()
           })
           .ToList();
            ViewBag.EnqStatusList = _db.Tbl_EnqStatusMasters
               .Where(x => x.IsDelete == false)
               .Select(x => new SelectListItem
               {
                   Value = x.Id.ToString(),
                   Text = (x.EnqStatus ?? "").Trim()
               })
               .ToList();
            ViewBag.InqTypeList = _db.Tbl_InqTypeMasters
                    .Where(x => x.IsDelete == false)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = (x.InqType ?? "").Trim()
                    })
                    .ToList();
            ViewBag.SubTypeList = _db.Tbl_SubTypeMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.SubType ?? "").Trim()
                })
                .ToList();
            ViewBag.EquipTypeList = _db.Tbl_EquipTypeMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EquipmentType ?? "").Trim()
                })
                .ToList();

            ViewBag.TEList = _db.Tbl_EngineerMasters
            .Where(x => x.IsDelete == false)
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = (x.EngineerName ?? "").Trim()
            })
            .ToList();
            ViewBag.PEList = _db.Tbl_EngineerMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EngineerName ?? "").Trim()
                })
                .ToList();

            ViewBag.EEList = _db.Tbl_EngineerMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EngineerName ?? "").Trim()
                })
                .ToList();
            return View(mm);



        }
        public ActionResult Delete(int id)
        {
            try
            {
                var inquiry = _db.Tbl_InquiryMasters.FirstOrDefault(x => x.Id == id && x.IsDelete == false);

                if (inquiry != null)
                {
                    // 1. Locate and delete the physical file from the disk
                    if (!string.IsNullOrEmpty(inquiry.UploadedFile))
                    {
                        string basePath = Server.MapPath("~/Docs/");
                        string fullPath = Path.Combine(basePath, inquiry.UploadedFile);

                        if (System.IO.File.Exists(fullPath))
                        {
                            try
                            {
                                System.IO.File.Delete(fullPath);
                            }
                            catch (Exception fileEx)
                            {
                                // Optional: Log file exclusion issues, but don't stop the DB update
                            }
                        }
                    }

                    // 2. Perform Soft Delete and clear the file tracking fields
                    inquiry.IsDelete = true;
                    inquiry.UploadedFile = string.Empty; // Clear out the filename
                    inquiry.FilePath = string.Empty;     // Clear out the file path

                    inquiry.ModifiedDate = DateTime.Now;
                    inquiry.ModifiedBy = Session["UserID"]?.ToString();

                    // 3. Persist changes to the Database
                    _db.Entry(inquiry).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();

                    TempData["Success"] = "Inquiry deleted successfully.";
                }
                else
                {
                    TempData["Error"] = "Inquiry not found.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UIndex()
        {
            string financialYear = Session["FinancialYear"]?.ToString();
            List<InqListViewModel> InquiryList = _dropDownCollection.GetInquiryList(financialYear);
            return View(InquiryList);

        }
        #endregion
    }
}