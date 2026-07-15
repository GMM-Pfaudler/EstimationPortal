using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.EFModelViews
{
    public class RoleAccessViewModel
    {
        public int? RoleId { get; set; }
        public string Role { get; set; }    
        public List<MainMenuViewModel> MainMenus { get; set; }
    }
    public class MainMenuViewModel
    {
        public int? MainMenuId { get; set; }
        public string MenuName { get; set; }
        public List<SubMenuViewModel> SubMenus { get; set; }
    }
    public class SubMenuViewModel
    {
        public int? SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public bool IsChecked { get; set; }
    }
}
