using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.EFModelViews
{
    public class LoginUserMasters
    {
        [Key]
        public int Id { get; set; }
        public int? ENGId { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string PAN { get; set; }
       // public string Department { get; set; }
        public DateTime? LoginFirstDT { get; set; }
        public bool Active { get; set; }
        public bool IsDelete { get; set; }
        // public string PO { get; set; }
    }
}
