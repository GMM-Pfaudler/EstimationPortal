using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.EFModelViews
{
    public class InqListViewModel
    {
        public int Id { get; set; }
        public string SFNo { get; set; }
        public string SalesPerson { get; set; } // masters
        public DateTime? InqReceiptDate { get; set; }
        public string InqType { get; set; } // masters
        public string DomExp { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ActualSubDate { get; set; }
        public string SubType { get; set; } // masters
        public string EnqStatus { get; set; }
        public string Client { get; set; }
        public string EPC { get; set; }
        public string EquipType { get; set; } // masters
        public decimal? TagQty { get; set; }
        public decimal? TotalQty { get; set; }
        public string MajorMOC { get; set; }
        public string DesignScope { get; set; }
        public string TE { get; set; }// masters
        public string PE { get; set; }// masters
        public string EE { get; set; }// masters
        public string TqNo { get; set; }
        public DateTime? TqReceivedDate { get; set; }
        public DateTime? TqDueDate { get; set; }
        public DateTime? TqSubDate { get; set; }
        public string ProjectNo { get; set; }
        public DateTime? ProjReceivedDate { get; set; }
        public string ProjStatus { get; set; }
        public string RsValue { get; set; }
        public string Remarks { get; set; }
        public string InqTypeId { get; set; }
        public string EnqStatusId { get; set; }
        public string ThEnggId { get; set; }
        public string PrEnggId { get; set; }
        public string EstEnggId { get; set; }
        public string FilePath { get; set; }
    }
}
