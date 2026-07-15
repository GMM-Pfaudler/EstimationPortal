using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.DTO
{
    public class LGH_DTO
    {
        [Key]
        public int id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime LoginStartTime { get; set; }
    }
}
