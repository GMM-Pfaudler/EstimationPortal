using SUP_CORE.DTO;
using SUP_DAL.EFContextProvider;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.SqlClient;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using SUP_CORE.EFModel;
using SUP_CORE.EFModelViews;
using System.Reflection.Emit;
using System.Xml.Linq;
using System.Data;
using dotless.Core.Parser.Infrastructure;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Bibliography;
namespace EstimationPortal.CustomeAttribute
{
    public class DropDownCollection
    {
        private readonly SupplierDbContext _db;
        public DropDownCollection(SupplierDbContext DbContext)
        {
            this._db = DbContext;
        }
        public List<TotalEnqQuoted> GetTotalEnqQuoted(string financialYear)
        {
            SqlParameter param = new SqlParameter("@FinancialYear", financialYear);
            return _db.Database.SqlQuery<TotalEnqQuoted>(
                "EXEC SP_GetTotalEnqQuoted @FinancialYear",
                param
            ).ToList();

            //return _db.Database.SqlQuery<TotalEnqQuoted>("SP_GetTotalEnqQuoted").ToList();
        }
        public List<InqListViewModel> GetDueInquiryList(string financialYear)
        {
            SqlParameter param = new SqlParameter("@FinancialYear", financialYear);

            return _db.Database.SqlQuery<InqListViewModel>(
                "EXEC SP_GetDueInqListViewModel @FinancialYear",
                param
            ).ToList();
        }
        public List<InqListViewModel> GetInquiryList(string financialYear)
        {
            SqlParameter param = new SqlParameter("@FinancialYear", financialYear);

            return _db.Database.SqlQuery<InqListViewModel>(
                "EXEC SP_GetInqListViewModel @FinancialYear",
                param
            ).ToList();

            //return _db.Database.SqlQuery<InqListViewModel>("SP_GetInqListViewModel").ToList();
        }
        public bool? CheckTqExist(string SFNo, int id)
        {
            var data = _db.Tbl_TqRegisters.Any(o => o.SFNo.ToString().Trim() == SFNo.Trim() && o.IsDelete == false && o.Id != id);
            return data;
        }
        public bool? CheckEnquiStatusExist(string EnqStatus, int id)
        {
            var data = _db.Tbl_EnqStatusMasters.Any(o => o.EnqStatus.Trim() == EnqStatus.Trim() && o.Id != id);
            return data;
        }
        public bool? CheckEmailIDExist(string Email_ID, int id)
        {
            var data = _db.Tbl_EmailMasters.Any(o => o.Email_ID.Trim() == Email_ID.Trim() && o.IsDelete == false && o.Id != id);
            return data;
        }
        public bool? CheckTagExist(string EngineerName,int id)
        {
            var data = _db.Tbl_EngineerMasters.Any(o => o.EngineerName.Trim() == EngineerName.Trim() && o.IsDelete == false && o.Id != id);
            return data;
        }
        public bool? CheckEquipTypeExist(string EquipmentType, int id)
        {
            var data = _db.Tbl_EquipTypeMasters.Any(o => o.EquipmentType.Trim() == EquipmentType.Trim() && o.Id != id);
            return data;
        }
        public bool? CheckSubTypeExist(string SubType, int id)
        {
            var data = _db.Tbl_SubTypeMasters.Any(o => o.SubType.Trim() == SubType.Trim() && o.Id != id);
            return data;
        }
        public bool? CheckInqTypeExist(string InqType, int id)
        {
            var data = _db.Tbl_InqTypeMasters.Any(o => o.InqType.Trim() == InqType.Trim() && o.Id != id);
            return data;
        }
        public bool? CheckUserExist(string UserID, int id)
        {
            var data = _db.Tbl_UserMasters.Any(o => o.UserID.Trim() == UserID.Trim() && o.IsDelete == false && o.Id != id);
            return data;
        }

        public SelectList UserList()
        {
            var users = _db.Tbl_UserMasters.
                Where(s=>s.IsDelete == false)
                .Select(s => new
                {
                    Id = s.Id,
                    UserName = s.UserName + " - " + s.UserID// Custom display text
                })
                .ToList();

            return new SelectList(users, "Id", "UserName");
        }

        
    }
}