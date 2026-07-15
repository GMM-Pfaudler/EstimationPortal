using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.EFModel
{ 
    public class Tbl_LoginLogs
    {
        [Key]
        public int id { get; set; } 
        public int UserId { get; set; } 
        public DateTime? LoginDate { get; set; }
        public DateTime? LogoutDate { get; set; }
        public string LoginToken { get; set; }
    }
}
