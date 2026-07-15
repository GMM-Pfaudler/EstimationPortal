using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.DTO
{
    public class RevHistoryDTO
    { 
        [Key]
        public int Id { get; set; }
        public string DocName { get; set; }
        public string Rev { get; set; }
        public string PlannedSubDate { get; set; }
        public DateTime? ActualSubDate { get; set; }
        public string RevFileName { get; set; }
        public DateTime? RecDate { get; set; }
        public string RecFileName { get; set; }
        public string SID { get; set; }
    }
}
