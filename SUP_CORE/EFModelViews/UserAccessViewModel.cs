using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.EFModelViews
{
    public class UserAccessViewModel
    {
        public int UserMaster_Id { get; set; }
        public int RoleId { get; set; }
        public List<MainMenuVM> MainMenus { get; set; }
    }
    public class MainMenuVM
    {
        public int MainMenuId { get; set; }
        public string MenuName { get; set; }
        public List<SubMenuVM> SubMenus { get; set; }
    }
    public class SubMenuVM
    {
        public int SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public bool IsChecked { get; set; }
    }
}
