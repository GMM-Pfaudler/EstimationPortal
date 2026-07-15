using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.DTO
{
    public class DocumentsListViewModel
    {
        public int Id { get; set; }
        public string SO { get; set; }
        public int ProjectMasterId { get; set; }  
        public string DocName { get; set; }
        public string GMMDocNumber { get; set; }
        public string ClientDocNumber { get; set; }
        public string Rev { get; set; }
        public string Discipline { get; set; }
        public string DocCategory { get; set; }
        public string PlannedSubDate { get; set; }
        public string ActualSubmitDate { get; set; }
        public string FileName { get; set; }
        public string RevFileName { get; set; } 
        public bool? IsFileUpload { get; set; } = false;
        public int? SID { get; set; } 
        public string StatusName { get; set; }
        public string Remarks { get; set; }
    }
}
