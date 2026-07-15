using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.DTO
{
    public class NewDocViewModel
    {
        public string DocName { get; set; }
        public string GMMDocNumber { get; set; }
        public string ClientDocNumber { get; set; }
        public string Rev { get; set; }
        public string Discipline { get; set; }
        public string DocCategory { get; set; }
        public string PlannedSubDate { get; set; }
    }
}
