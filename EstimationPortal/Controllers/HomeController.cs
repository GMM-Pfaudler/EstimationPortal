using Castle.Core.Logging;

using SUP_DAL.EFContextProvider;
using EstimationPortal.CustomeAttribute;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Text.Json;
using DocumentFormat.OpenXml.Office2010.Excel;
using SUP_CORE.DTO;
using SUP_BAL.IRepository;
using SUP_CORE.EFModel;
using SUP_CORE.EFModelViews;
using Microsoft.Ajax.Utilities;
using EstimationPortal.CustomeAttribute;
using System.Data.Entity;
using Castle.MicroKernel;
using DocumentFormat.OpenXml.Math;
using WebGrease.Css.Ast;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Vml;
using dotless.Core.Utils;
using dotless.Core.Parser.Infrastructure;

namespace EstimationPortal.Controllers
{
    [SessionTimeout]
    [NoDirectAccess]
    public class HomeController : Controller
    {
        private SupplierDbContext _db;
        private DropDownCollection _dropDownCollection;

        public HomeController(SupplierDbContext db, DropDownCollection dropDownCollection)
        {
            _dropDownCollection = dropDownCollection;
            _db = db;

        }

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["RoleID"] != null && Session["RoleID"].ToString() == "3")
            {
                return View("UIndex");
            }
            string FinancialYear = Session["FinancialYear"]?.ToString();
            var mm = new DashboardViewModel();
            mm.ManPowerLoadings = new List<ManPowerLoading>();
            mm.ManPowerTags = new List<ManPowerTag>();
            mm.DomExpChartData = new List<DomExpChart>();
            mm.EnquiryStatusChartData = new List<EnquiryStatusChart>();
            mm.InquiryTypeChartData = new List<InquiryTypeChart>();
            mm.DueInquiryList = new List<InqListViewModel>();
            mm.TotalEnqSummry = new List<TotalEnqQuoted>();
            mm.EnqQuotedSalesSummry = new List<EnqQuotedSales>();

            var Engineers = _db.Tbl_EngineerMasters
                .Where(e => e.IsDelete == false).ToList();
            // start Individual Summary of Enquiries quoted
            foreach (var eng in Engineers)
            {
                decimal totalFirm = _db.Tbl_InquiryMasters
                    .Where(x => x.FinancialYear == FinancialYear && x.IsDelete == false
                             && x.InqType == "2"
                             && (x.TE == eng.Id.ToString()
                              || x.PE == eng.Id.ToString()
                              || x.EE == eng.Id.ToString())).ToList()
                        .Sum(x =>
                        {
                            decimal value;
                            return decimal.TryParse(x.RsValue, out value) ? value : 0;
                        });

                decimal totalBudgetary = _db.Tbl_InquiryMasters
                                .Where(x => x.FinancialYear == FinancialYear && x.IsDelete == false
                                         && x.InqType == "1"
                                         && (x.TE == eng.Id.ToString()
                                          || x.PE == eng.Id.ToString()
                                          || x.EE == eng.Id.ToString()))
                                .ToList()
                                .Sum(x =>
                                {
                                    decimal value;
                                    return decimal.TryParse(x.RsValue, out value) ? value : 0;
                                });

                decimal totalOrderReceived = _db.Tbl_InquiryMasters
                               .Where(x => x.FinancialYear == FinancialYear && x.IsDelete == false
                                        && x.ProjStatus == "Won"
                                        && (x.TE == eng.Id.ToString()
                                         || x.PE == eng.Id.ToString()
                                         || x.EE == eng.Id.ToString()))
                               .ToList()
                               .Sum(x =>
                               {
                                   decimal value;
                                   return decimal.TryParse(x.RsValue, out value) ? value : 0;
                               });
                decimal strikeRate = 0;

                if (totalFirm > 0)
                {
                    strikeRate = (totalOrderReceived / totalFirm) * 100;
                }
                mm.EnqQuotedSummry.Add(new EnqQuoted
                {
                    EngineerName = eng.EngineerName,
                    TotalFirm = totalFirm.ToString("N2"),
                    TotalBudgetary = totalBudgetary.ToString("N2"),
                    TotalOrderReceived = totalOrderReceived.ToString("N2"),
                    StrikeRate = strikeRate.ToString("N2") + " %"
                });
            }
            // end Individual Summary of Enquiries quoted

            // start EnqQuotedSalesSummry
            decimal totalOrderBooking = _db.Tbl_InquiryMasters
                       .Where(x => x.FinancialYear == FinancialYear && x.IsDelete == false
                                && x.ProjStatus == "Won").AsEnumerable()
                            .Sum(x => decimal.TryParse(x.RsValue, out decimal value) ? value : 0);
            foreach (var eng in Engineers)
            {
                // totalFirm & totalBudgetary Like totalOrderReceived
                //decimal totalFirm = _db.Tbl_InquiryMasters
                //.Where(x => x.IsDelete == false
                //         && x.InqType == "2")
                //.ToList()
                //.Sum(x => GetEngineerShare(x, eng.Id.ToString()));

                //decimal totalBudgetary = _db.Tbl_InquiryMasters
                //    .Where(x => x.IsDelete == false
                //             && x.InqType == "1")
                //    .ToList()
                //    .Sum(x => GetEngineerShare(x, eng.Id.ToString()));

                decimal totalFirm = _db.Tbl_InquiryMasters
                    .Where(x => x.FinancialYear == FinancialYear && x.IsDelete == false
                             && x.InqType == "2"
                             && (x.TE == eng.Id.ToString()
                              || x.PE == eng.Id.ToString()
                              || x.EE == eng.Id.ToString())).ToList()
                        .Sum(x =>
                        {
                            decimal value;
                            return decimal.TryParse(x.RsValue, out value) ? value : 0;
                        });

                decimal totalBudgetary = _db.Tbl_InquiryMasters
                                .Where(x =>x.FinancialYear == FinancialYear && x.IsDelete == false
                                         && x.InqType == "1"
                                         && (x.TE == eng.Id.ToString()
                                          || x.PE == eng.Id.ToString()
                                          || x.EE == eng.Id.ToString()))
                                .ToList()
                                .Sum(x =>
                                {
                                    decimal value;
                                    return decimal.TryParse(x.RsValue, out value) ? value : 0;
                                });

                decimal totalOrderReceived = _db.Tbl_InquiryMasters
                        .Where(x => x.FinancialYear == FinancialYear && x.IsDelete == false
                                 && x.ProjStatus == "Won")
                        .ToList()
                        .Sum(x => GetEngineerShare(x, eng.Id.ToString()));
                decimal strikeRate = 0;

                if (totalFirm > 0)
                {
                    //strikeRate = (totalOrderReceived / totalFirm) * 100;
                    strikeRate = (totalOrderReceived / totalOrderBooking) * 100;
                }
                mm.EnqQuotedSalesSummry.Add(new EnqQuotedSales
                {
                    EngineerName = eng.EngineerName,
                    TotalFirm = totalFirm.ToString("N2"),
                    TotalBudgetary = totalBudgetary.ToString("N2"),
                    TotalOrderReceived = totalOrderReceived.ToString("N2"),
                    StrikeRate = strikeRate.ToString("N2") + " %"
                });
            }
            // end EnqQuotedSalesSummry
            var manPowerData = _db.Tbl_EngineerMasters
                .Where(e => e.IsDelete == false)
                .Select(e => new
                {
                    EngineerName = e.EngineerName,

                    ThermalDesignCount = _db.Tbl_InquiryMasters
                        .Count(i => i.TE == e.Id.ToString() && i.EnqStatus == "2" && i.IsDelete == false && i.FinancialYear == FinancialYear),

                    MechnicalDesignCount = _db.Tbl_InquiryMasters
                        .Count(i => i.PE == e.Id.ToString() &&
                                    (i.EnqStatus == "2" || i.EnqStatus == "12") && i.IsDelete == false && i.FinancialYear == FinancialYear),

                    EstimationCount = _db.Tbl_InquiryMasters
                        .Count(i => i.EE == e.Id.ToString() &&
                                    (i.EnqStatus == "2" || i.EnqStatus == "12") && i.IsDelete == false && i.FinancialYear == FinancialYear),

                    TQcount = (
                        from i in _db.Tbl_InquiryMasters
                        join t in _db.Tbl_TqRegisters
                            on i.Id.ToString() equals t.SFNo
                        where i.PE == e.Id.ToString() && i.IsDelete == false && i.FinancialYear == FinancialYear
                              && new[] { "TQ 1", "TQ 2", "TQ 3", "TQ 4", "TQ 5" }
                              .Contains(t.TqNo)
                        select t.Id
                    ).Count(),

                    TotalFirmDesigns = _db.Tbl_InquiryMasters
                        .Where(i => i.FinancialYear == FinancialYear &&
                            (i.TE == e.Id.ToString() || i.PE == e.Id.ToString()) &&
                            i.InqType == "2" && i.IsDelete == false &&
                            (i.EnqStatus == "2" || i.EnqStatus == "12") &&
                            (i.DesignScope == "Mechanical" ||
                             i.DesignScope == "Thermal + Mechanical"))
                        .Sum(i => (decimal?)i.TagQty) ?? 0,

                    TotalBudgetaryDesigns = _db.Tbl_InquiryMasters
                        .Where(i => i.FinancialYear == FinancialYear &&
                            (i.TE == e.Id.ToString() || i.PE == e.Id.ToString()) &&
                            i.InqType == "1" && i.IsDelete == false && 
                            (i.EnqStatus == "2" || i.EnqStatus == "12") &&
                            (i.DesignScope == "Mechanical" ||
                             i.DesignScope == "Thermal + Mechanical"))
                        .Sum(i => (decimal?)i.TagQty) ?? 0,

                    TotalFirmEstimates = _db.Tbl_InquiryMasters
                        .Where(i => i.FinancialYear == FinancialYear &&
                            i.EE == e.Id.ToString() &&
                            i.InqType == "2" && i.IsDelete == false &&
                            (i.EnqStatus == "2" || i.EnqStatus == "12") &&
                            (i.SubType == "2" || i.SubType == "3"))
                        .Sum(i => (decimal?)i.TagQty) ?? 0,

                    TotalBudgetaryEstimates = _db.Tbl_InquiryMasters
                        .Where(i => i.FinancialYear == FinancialYear &&
                            i.EE == e.Id.ToString() &&
                            i.InqType == "1" && i.IsDelete == false &&
                            (i.EnqStatus == "2" || i.EnqStatus == "12") &&
                            (i.SubType == "2" || i.SubType == "3"))
                        .Sum(i => (decimal?)i.TagQty) ?? 0
                })
                .AsEnumerable()
                .Select(x => new ManPowerLoading
                {
                    EngineerName = x.EngineerName,
                    ThermalDesignCount = x.ThermalDesignCount.ToString(),
                    //MechnicalDesignCount = x.MechnicalDesignCount.ToString(),
                    //EstimationCount = x.EstimationCount.ToString(),
                    //TQcount = x.TQcount.ToString(),
                    Total_Enquiries = (
                        x.ThermalDesignCount
                         + x.MechnicalDesignCount +
                        x.EstimationCount +
                        x.TQcount
                    ).ToString(),
                    TotalFirmDesigns = x.TotalFirmDesigns.ToString(),
                    TotalBudgetaryDesigns = x.TotalBudgetaryDesigns.ToString(),
                    TotalFirmEstimates = x.TotalFirmEstimates.ToString(),
                    TotalBudgetaryEstimates = x.TotalBudgetaryEstimates.ToString()
                })
             .ToList();
            
            mm.ManPowerLoadings = manPowerData.ToList();

            /* start Manpower Tag */
            var manPowerTagData = _db.Tbl_EngineerMasters
                .Where(e => e.IsDelete == false)
                .Select(e => new
                {
                    EngineerName = e.EngineerName,

                    TotalThermalDesigns = _db.Tbl_InquiryMasters
                        .Where(i => i.FinancialYear == FinancialYear &&
                            (i.TE == e.Id.ToString()) &&
                            (i.InqType == "1" || i.InqType == "2") && i.IsDelete == false &&
                            (i.EnqStatus == "3") &&
                            (i.DesignScope == "Thermal" ||
                             i.DesignScope == "Thermal + Mechanical"))
                        .Sum(i => (decimal?)i.TagQty) ?? 0,

                    TotalFirmDesigns = _db.Tbl_InquiryMasters
                        .Where(i => i.FinancialYear == FinancialYear &&
                            (i.PE == e.Id.ToString()) &&
                            (i.InqType == "2") && i.IsDelete == false &&
                            (i.EnqStatus == "3") &&
                            (i.DesignScope == "Mechanical" ||
                             i.DesignScope == "Thermal + Mechanical"))
                        .Sum(i => (decimal?)i.TagQty) ?? 0,

                    TotalFirmEstimates = _db.Tbl_InquiryMasters
                        .Where(i => i.FinancialYear == FinancialYear &&
                            i.EE == e.Id.ToString() &&
                            i.InqType == "2" && i.IsDelete == false &&
                            (i.EnqStatus == "3"))
                        .Sum(i => (decimal?)i.TagQty) ?? 0,

                    TotalBudgetaryDesigns = _db.Tbl_InquiryMasters
                        .Where(i => i.FinancialYear == FinancialYear &&
                            (i.PE == e.Id.ToString()) &&
                            (i.InqType == "1") && i.IsDelete == false &&
                            (i.EnqStatus == "3") &&
                            (i.DesignScope == "Mechanical" ||
                             i.DesignScope == "Thermal + Mechanical"))
                        .Sum(i => (decimal?)i.TagQty) ?? 0,

                    TotalBudgetaryEstimates = _db.Tbl_InquiryMasters
                        .Where(i => i.FinancialYear == FinancialYear &&
                            i.EE == e.Id.ToString() &&
                            i.InqType == "1" && i.IsDelete == false &&
                            (i.EnqStatus == "3"))
                        .Sum(i => (decimal?)i.TagQty) ?? 0
                    
                })
                .AsEnumerable()
                .Select(x => new ManPowerTag
                {
                    EngineerName = x.EngineerName,
                    TotalThermalDesigns=x.TotalThermalDesigns.ToString(),
                    TotalFirmDesigns = x.TotalFirmDesigns.ToString(),
                    TotalFirmEstimates = x.TotalFirmEstimates.ToString(),
                    TotalBudgetaryDesigns=x.TotalBudgetaryDesigns.ToString(),
                    TotalBudgetaryEstimates=x.TotalBudgetaryEstimates.ToString()
                })
             .ToList();
            mm.ManPowerTags = manPowerTagData.ToList();
            /* end Manpower Tag */

            mm.DomExpChartData = _db.Tbl_InquiryMasters
                       .Where(s=>s.IsDelete == false && s.FinancialYear == FinancialYear && !string.IsNullOrEmpty(s.DomExp))
                       .GroupBy(s => s.DomExp)
                       .Select(g => new DomExpChart
                       {
                           TypeName = g.Key,
                           TotalTagQty = g.Sum(s => s.TagQty ?? 0)
                       })
                       .ToList();
            mm.EnquiryStatusChartData = (
                    from status in _db.Tbl_EnqStatusMasters
                    where status.IsDelete == false

                    join inquiry in _db.Tbl_InquiryMasters.Where(x => x.IsDelete == false && x.FinancialYear == FinancialYear)
                        on status.Id.ToString() equals inquiry.EnqStatus into inquiryGroup

                    select new EnquiryStatusChart
                    {
                        StatusName = status.EnqStatus,
                        InquiryCount = inquiryGroup.Count()
                    }
                ).ToList();
            mm.InquiryTypeChartData = (
                    from inq in _db.Tbl_InqTypeMasters
                    where inq.IsDelete == false

                    join inquiry in _db.Tbl_InquiryMasters.Where(x => x.IsDelete == false && x.FinancialYear == FinancialYear)
                        on inq.Id.ToString() equals inquiry.InqType into inquiryGroup

                    select new InquiryTypeChart
                    {
                        InqType = inq.InqType,
                        InquiryCount = inquiryGroup.Count()
                    }
                ).ToList();
            mm.DueInquiryList = _dropDownCollection.GetDueInquiryList(FinancialYear);
            /* start Total enquiries quoted */
            mm.TotalEnqSummry = _dropDownCollection.GetTotalEnqQuoted(FinancialYear);

            mm.TotalOrder_Received = _db.Tbl_InquiryMasters
                .Where(x => x.IsDelete == false && x.FinancialYear == FinancialYear && x.ProjStatus == "Won")
                .AsEnumerable()
                .Sum(x => decimal.TryParse(x.RsValue, out decimal val) ? val : 0);

            var summary = mm.TotalEnqSummry.FirstOrDefault();

            decimal strike_Rate = 0;

            if (summary != null && summary.TotalFirmQuoted > 0)
            {
                strike_Rate = Math.Round(
                    (mm.TotalOrder_Received / summary.TotalFirmQuoted.Value) * 100, 2);
            }

            mm.StrikeRateText = strike_Rate.ToString("N2") + "%";
            /* end Total enquiries quoted */
            
            //DashboardViewModel model = new DashboardViewModel
            //{
            //    ManPowerLoadings = manPowerData
            //};

            return View(mm);

            
        }
        private decimal GetEngineerShare(Tbl_InquiryMasters inquiry, string engineerId)
        {
            decimal rsValue;
            if (!decimal.TryParse(inquiry.RsValue, out rsValue))
                return 0;

            // Get distinct engineers assigned to the inquiry
            var engineers = new List<string>
            {
                inquiry.TE,
                inquiry.PE,
                inquiry.EE
            }
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();
            if (engineers.Count == 0)
                return 0;
            int engineerOccurrences = engineers.Count(x => x == engineerId);
            return (rsValue / engineers.Count) * engineerOccurrences;
        }
    }
}