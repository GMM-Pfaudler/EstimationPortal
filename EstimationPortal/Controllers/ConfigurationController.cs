using Castle.Core.Logging;
using SUP_BAL.IRepository;
using SUP_CORE.EFModel;
using SUP_CORE.EFModelViews;
using EstimationPortal.CustomeAttribute;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity.ModelConfiguration.Conventions;
using System;
using DocumentFormat.OpenXml.EMMA;
using SUP_DAL.EFContextProvider;
using Microsoft.Ajax.Utilities;
using EstimationPortal.App_Start;
using EstimationPortal.CustomeAttribute;
using EstimationPortal.App_Start;

namespace EstimationPortal.Controllers
{
    public class ConfigurationController : Controller
    {
        // GET: Configuration
        private readonly ILogger _logger;
        private readonly IGenericRepository<Tbl_UserMasters> _genericUserMasterRepository;
        //private IGenericRepository<LoginUserMasters> _genericUsesrMasterRepository;
        private DropDownCollection _dropDownCollection;
        private readonly SupplierDbContext _db;
        public ConfigurationController(ILogger logger, SupplierDbContext db, IGenericRepository<Tbl_UserMasters> genericUserMasterRepository, DropDownCollection dropDownCollection)
        {
            _logger = logger;
            _genericUserMasterRepository = genericUserMasterRepository; 
            _dropDownCollection = dropDownCollection; 
            _db = db;
        }
        [NoDirectAccess]
        public PartialViewResult NavBarInit()
        {
            var pUsrName = Session["UserName"].ToString();
            var datadb = _genericUserMasterRepository.Find(s => s.UserName.Trim() == pUsrName.Trim() && s.IsDelete == false).FirstOrDefault();
           
            return PartialView("~/Views/Shared/_SidebarNav.cshtml");
        }

        [SessionExpire] 
        [HttpGet]
        [NoDirectAccess]
        public ActionResult EditUserAccess()
        {
            ViewBag.UserList = _dropDownCollection.UserList();
            var model = new UserAccessViewModel
            {
                UserMaster_Id = 0,
                RoleId=0,
                MainMenus = new List<MainMenuVM>()  // empty list so no null reference
            };

            return View(model);
            
        }

        

        //[HttpPost]
        //public ActionResult GetUserAccessDetails(int? RoleID)
        //{
        //    ViewBag.mainMenuList = _genericRepository.GetAll();
        //    var getUserId = _genericUserMasterRepository.Find(s => s.RoleId == RoleID).FirstOrDefault();
        //    var pList = _userAccessMasterRepository.GetAll(t => t.UserMaster_Id == getUserId.Id).ToList().Count != 0 ? _userAccessMasterRepository.GetAll(t => t.UserMaster_Id == getUserId.Id).ToList() : new List<UserAccessMaster>();
        //    pList = pList.Count() == 0 ? new List<UserAccessMaster>()
        //    {
        //        new UserAccessMaster { UserMaster_Id = getUserId.Id }
        //    } : pList;
        //    return PartialView("~/Views/configuration/_UserAccessForm.cshtml", pList);
        //}
    }
}