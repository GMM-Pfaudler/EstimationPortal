using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.EFModelViews
{
    public class DashboardViewModel
    {
        public decimal TotalOrder_Received { get; set; }
        public string StrikeRateText { get; set; }
        public List<ManPowerLoading> ManPowerLoadings { get; set; }
        public List<ManPowerTag> ManPowerTags { get; set; }
        public List<DomExpChart> DomExpChartData { get; set; } = new List<DomExpChart>();
        public List<EnquiryStatusChart> EnquiryStatusChartData { get; set; } = new List<EnquiryStatusChart>();
        public List<InquiryTypeChart> InquiryTypeChartData { get; set; } = new List<InquiryTypeChart>();
        public List<InqListViewModel> DueInquiryList { get; set; } = new List<InqListViewModel>();
        public List<EnqQuoted> EnqQuotedSummry { get; set; } = new List<EnqQuoted>();
        public List <TotalEnqQuoted> TotalEnqSummry { get; set; } = new List<TotalEnqQuoted>();
        public List<EnqQuotedSales> EnqQuotedSalesSummry { get; set; } = new List<EnqQuotedSales>();
    }
    public class TotalEnqQuoted
    {
        public decimal? TotalFirmQuoted { get; set; }
        public decimal? TotalBudgetaryQuoted { get; set; }
        public decimal? GrandTotal { get; set; }
    }
    public class EnqQuotedSales
    {
        public string EngineerName { get; set; }
        public string TotalFirm { get; set; }
        public string TotalBudgetary { get; set; }
        public string TotalOrderReceived { get; set; }
        public string StrikeRate { get; set; }
    }
    public class EnqQuoted
    {
        public string EngineerName { get; set; }
        public string TotalFirm { get; set; }
        public string TotalBudgetary { get; set; }
        public string TotalOrderReceived { get; set; }
        public string StrikeRate { get; set; }
    }
    public class ManPowerTag
    {
        public string EngineerName { get; set; }
        public string TotalThermalDesigns { get; set; }
        public string TotalFirmDesigns { get; set; }
        public string TotalFirmEstimates { get; set; }
        public string TotalBudgetaryDesigns { get; set; }
        public string TotalBudgetaryEstimates { get; set; }
        
        
        
        
    }
    public class ManPowerLoading 
    { 
        public string EngineerName { get; set; }
        public string ThermalDesignCount { get; set; }
        public string MechnicalDesignCount { get; set; }
        public string EstimationCount { get; set; }
        public string TQcount { get; set; }
        public string Total_Enquiries { get; set; }
        public string TotalFirmDesigns { get; set; }
        public string TotalBudgetaryDesigns { get; set; }
        public string TotalFirmEstimates { get; set; }
        public string TotalBudgetaryEstimates { get; set; }
    }
    public class DomExpChart
    {
        public string TypeName { get; set; }
        public decimal? TotalTagQty { get; set; }
    }
    public class EnquiryStatusChart
    {
        public string StatusName { get; set; }
        public int InquiryCount { get; set; }
    }
    public class InquiryTypeChart
    {
        public string InqType { get; set; }
        public int InquiryCount { get; set; }
    }
}
