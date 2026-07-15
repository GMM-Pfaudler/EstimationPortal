using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.EFModelViews
{
    public class UsersList
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string EmployeeCode { get; set; }
        //public int? RoleId {  get; set; }
        public bool? IsActive { get; set; }
    }
}
