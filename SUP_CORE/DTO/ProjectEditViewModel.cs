using SUP_CORE.EFModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.DTO
{
    public class ProjectEditViewModel
    {
        public ProjectMasters Project { get; set; }
        public List<DocumentsListViewModel> DocMasters { get; set; }
        public List<NewDocViewModel> NewDocs { get; set; } // Add this line
    }
}
