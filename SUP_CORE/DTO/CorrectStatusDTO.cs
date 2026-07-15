using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.DTO
{
    public  class CorrectStatusDTO
    {
        public List<DocItemDTO> DocList { get; set; }
       
    }
    public class DocItemDTO
    {
        public int? DocId { get; set; }
        public int? RevId { get; set; }
        public int? PojectId { get; set; }
        public string DocName { get; set; }
        public string GMMDocNumber { get; set; }
        public string ClientDocNumber { get; set; }
        public string Rev { get; set; }
        public string Discipline { get; set; }
        public string PlannedSubDate { get; set; }
        public string StatusID { get; set; }
        public string RecDate { get; set; }
        public string Facility { get; set; }
        // Add this property to bind the selected status ID from dropdown:
        public int SID { get; set; }
    }
}
