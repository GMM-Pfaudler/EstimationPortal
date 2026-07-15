using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.DTO
{
    public class PieDataDTO
    {
        public string PO { get; set; }
        public string SO { get; set; }
        public string Client {  get; set; }
        public string DocName { get; set; } 
        public string GMMDocNumber { get; set; }
        public string ClientDocNumber { get; set; }
        public string Rev {  get; set; }
        public string Discipline { get; set; }  
        public bool? IsHold {  get; set; }
        public bool? IsClosed { get; set; }
    }
}
