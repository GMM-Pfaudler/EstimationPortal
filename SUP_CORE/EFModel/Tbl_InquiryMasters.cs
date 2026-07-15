using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SUP_CORE.EFModel
{
    public class Tbl_InquiryMasters : BaseModel
    {
        [key]
        public int Id { get; set; }
        public string FinancialYear { get; set; }
        [Required(ErrorMessage = "Required")]
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
       
        public string ProjectNo { get; set; }
        public DateTime? ProjReceivedDate { get; set; }
        public string ProjStatus { get; set; }
        public string RsValue { get; set; }
        public string Remarks { get; set; }

        //[NotMapped]
        //public string Client { get; set; }
        //[NotMapped]
        //public string TqNo { get; set; }
        //[NotMapped]
        //public DateTime? TqReceivedDate { get; set; }
        //[NotMapped]
        //public DateTime? TqDueDate { get; set; }
        //[NotMapped]
        //public DateTime? TqSubDate { get; set; }
        public string UploadedFile { get; set; }
        public string FilePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase SelectedFile { get; set; }
        [NotMapped]
        public bool IsMailTrigger { get; set; }
        [NotMapped]
        public string MailNotes { get; set; }
    }
}
