using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.EFModel
{
    public class Tbl_InqTypeMasters : BaseModel
    {
        [key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Inquiry Type is required")]
        public string InqType { get; set; }
    }
}
