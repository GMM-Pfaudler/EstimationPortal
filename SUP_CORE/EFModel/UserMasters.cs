using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.EFModel
{
    public partial class Tbl_UserMasters : BaseModel
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public int? RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Tbl_Roles Tbl_Roles { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public string UserID { get; set; }


        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get;set; }
         

        [Required(ErrorMessage = "Email Address is required")]
        public string EmailID { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
