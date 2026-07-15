using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.DTO
{
    public class QLT_PODTO
    {
        public int Id { get; set; }
        public int? EngId { get; set; }
        public string CTNumbr { get; set; }
        public string Order_Number { get; set; }
        public int? Line_Number { get; set; }
        public string Sequence { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public string Ordered_Quantity { get; set; }
        public string Pending_Quantity { get; set; }
        public string Unit { get; set; }
        public string Purchase_Price { get; set; }
        public string Currency { get; set; }
        public string Size { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Order_Date { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Planned_RC_DateHeader { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Planned_RC_DateLine { get; set; }
        public string Warehouse { get; set; }
        public string Buyer { get; set; }
        //[DataType(DataType.DateTime)]
        public DateTime? SuppOfferDate { get; set; }
        public string Drawing_Number { get; set; }
        public string Revision_Number { get; set; }
        public string Supplier_ID { get; set; }
        public string Sales_Order { get; set; }
        public int TotalRows { get; set; }
        public int? TrType { get; set; }
        public bool? OffrAcceptReject { get; set; }
        public bool? InspectionW { get; set; }
        public decimal? Qty_Offered { get; set; }
        public decimal? QtyUse { get; set; }
        public string Supp_Remarks { get; set; }
        public string GMM_Drw_Numbr { get; set; }
        public DateTime? Supp_InspectionDate { get; set; }
        public string Purchase_Remarks { get; set; }
        public string Status { get; set; }
        public decimal? Qty_ApprovedReject { get; set; }
        public string QLT_Remarks { get; set; }
        public string Name { get; set; }
        public string GMM_Insp_File { get; set; }
        public string QLTInspStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string RReason { get; set; } 
        public string BP_Name { get; set; }
        public string GMM_Drw_Rev { get; set; }
        public DateTime? QAssignedDate { get; set; }
        public DateTime? InspPlannedDate { get; set; }
        public DateTime? EngAttendedDate { get; set; } 
        public string Buyer_Code { get; set; }
        public string Office { get; set; }
        public string Remarks { get; set; }
        public string QIEngineer { get; set; }
        public decimal? ActualQty { get; set; }
    }
}
