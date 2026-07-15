using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.EFModel
{
    public class Tbl_Roles : BaseModel
    {
        [key]
        public int Id { get; set; } 
        public string RoleName { get; set; }
    }
}
