using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.EFModel
{
    public class Tbl_SubTypeMasters : BaseModel
    {
        [key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Sub Type is required")]
        public string SubType { get; set; }
    }
}
