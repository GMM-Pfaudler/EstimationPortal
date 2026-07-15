using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Math;
using EstimationPortal.CustomeAttribute;
using SUP_CORE.EFModelViews;
using SUP_DAL.EFContextProvider;

namespace EstimationPortal.Controllers
{
    [SessionTimeout]
    [NoDirectAccess]
    public class ReportController : Controller
    {
        private DropDownCollection _dropDownCollection;
        private readonly SupplierDbContext _db;
        public ReportController(SupplierDbContext db, DropDownCollection dropDownCollection)
        {
            _dropDownCollection = dropDownCollection;
            _db = db;
        }
        #region Inquiry Summary
        [HttpGet]
        public ActionResult Index(string ProjectNo, string InqType, string DomExp, string EnqStatus, string DueDate, string ThEngg, string PrEngg, string EstEngg, string ProjStatus)
        {
            string financialYear = Session["FinancialYear"]?.ToString();
            ViewBag.ProjectNoList = _db.Tbl_InquiryMasters
                .Where(x => x.FinancialYear == financialYear && x.IsDelete == false && x.ProjectNo != null && x.ProjectNo != "")
                .GroupBy(x => x.ProjectNo.Trim())
                .Select(g => new SelectListItem
                {
                    Value = g.FirstOrDefault().Id.ToString(),
                    Text = g.Key
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
            ViewBag.DomExpList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Domestic", Value = "Domestic" },
                new SelectListItem { Text = "Export", Value = "Export" }
            };
            ViewBag.EnqStatusList = _db.Tbl_EnqStatusMasters
                .Where(x => x.IsDelete == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.EnqStatus ?? "").Trim()
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
            ViewBag.ProjStatusList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Won", Value = "Won" },
                new SelectListItem { Text = "Lost", Value = "Lost" },
                new SelectListItem { Text = "Closed", Value = "Closed" },
                new SelectListItem { Text = "Unknown", Value = "Unknown" },
                new SelectListItem { Text = "Live", Value = "Live" }
            };
            List<InqListViewModel> InquiryList = _dropDownCollection.GetInquiryList(financialYear);
            if (!string.IsNullOrEmpty(ProjectNo))
            {
                InquiryList = InquiryList.Where(x => x.Id.ToString() == ProjectNo).ToList();
            }
            if (!string.IsNullOrEmpty(InqType))
            {
                InquiryList = InquiryList.Where(x => x.InqTypeId == InqType).ToList();
            }
            if (!string.IsNullOrEmpty(DomExp))
            {
                InquiryList = InquiryList.Where(x => x.DomExp == DomExp).ToList();
            }
            if (!string.IsNullOrEmpty(EnqStatus))
            {
                InquiryList = InquiryList.Where(x => x.EnqStatusId == EnqStatus).ToList();
            }
            if (!string.IsNullOrEmpty(DueDate))
            {
                DateTime dueDateValue = Convert.ToDateTime(DueDate);

                InquiryList = InquiryList
                    .Where(x => x.DueDate.HasValue &&
                                x.DueDate.Value.Date == dueDateValue.Date)
                    .ToList();
            }
            if (!string.IsNullOrEmpty(ThEngg))
            {
                InquiryList = InquiryList.Where(x => x.ThEnggId == ThEngg).ToList();
            }
            if (!string.IsNullOrEmpty(PrEngg))
            {
                InquiryList = InquiryList.Where(x => x.PrEnggId == PrEngg).ToList();
            }
            if (!string.IsNullOrEmpty(EstEngg))
            {
                InquiryList = InquiryList.Where(x => x.EstEnggId == EstEngg).ToList();
            }
            if (!string.IsNullOrEmpty(ProjStatus))
            {
                InquiryList = InquiryList.Where(x => x.ProjStatus.Trim() == ProjStatus.Trim()).ToList();
            }
            ViewBag.SelectedProjectNo = ProjectNo;
            ViewBag.SelectedInqType = InqType;
            ViewBag.SelectedDomExp = DomExp;
            ViewBag.SelectedEnqStatus = EnqStatus;
            ViewBag.SelectedThEngg = ThEngg;
            ViewBag.SelectedPrEngg = PrEngg;
            ViewBag.SelectedEstEngg = EstEngg;
            ViewBag.SelectedProjStatus = ProjStatus;
            return View(InquiryList);
        }
        #endregion 
    }
}