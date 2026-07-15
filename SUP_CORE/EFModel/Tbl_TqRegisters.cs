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
    public class Tbl_TqRegisters : BaseModel
    {
        [key]
        public int Id { get; set; }
        public string FinancialYear { get; set; }
        [Required(ErrorMessage = "Required")]
        public string SFNo { get; set; }
        public string TqNo { get; set; }
        public DateTime? TqReceivedDate { get; set; }
        public DateTime? TqDueDate { get; set; }
        public DateTime? TqSubDate { get; set; }
        public string Remarks { get; set; }

        [NotMapped]
        public string Client { get; set; }
        [NotMapped]
        public string TE { get; set; }
        [NotMapped]
        public string PE { get; set; }
        [NotMapped]
        public string EE { get; set; }

        public string UploadedFile { get; set; }
        public string FilePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase SelectedFile { get; set; }

    }
}
