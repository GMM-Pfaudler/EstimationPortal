using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.EFModel
{
    public class LoginHistory
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime? LoginStartTime { get; set; }
        public string UserIP { get; set; }
    }
}
