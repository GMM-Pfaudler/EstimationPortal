using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.DTO
{
    public class ProjectsListViewModel
    {
        [Key]
        public int Id { get; set; }
        public int? SID { get; set; }
        public string PO { get; set; }
        public string Client { get; set; }
        public string SO { get; set; }
        public int? QTY { get; set; }
        public string Facility { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? TurnAroundDays { get; set; }
        public string PMOUsers { get; set; }
        public string QLTUsers { get; set; }
        public string WLDUsers { get; set; }
        public string ENGUsers { get; set; }
        public bool? PMOSubmit { get; set; }
        public bool? QLTSubmit { get; set; }
        public bool? WLDSubmit { get; set; }
        public bool? ENGSubmit { get; set; }
        public string StatusName { get; set; }
    }
}
