using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.EFModel
{
    public class Tbl_EngineerMasters : BaseModel
    {
        [key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Engineer Name is required")]
        public string EngineerName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string EnggEmail { get; set; }


    }
}
