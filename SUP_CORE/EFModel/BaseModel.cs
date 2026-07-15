using System;
using System.Collections.Generic;
using System.Text;

namespace SUP_CORE.EFModel
{
    public class BaseModel
    {
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsDelete { get; set; } = false; 
    }
}
