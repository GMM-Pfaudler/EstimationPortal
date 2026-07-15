using SUP_BAL.IRepository;
using SUP_CORE.DTO;
using SUP_CORE.EFModel;
using SUP_DAL.EFContextProvider;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SUP_BAL.BAL
{
    public class SuppBAL
    {
        private readonly SupplierDbContext _db;
        private readonly IGenericRepository<UserMasters> _genericSuppTrRepository;
        public SuppBAL(SupplierDbContext SupplrContext, IGenericRepository<UserMasters> genericSuppTrRepository)
        {
            this._db = SupplrContext;
            _genericSuppTrRepository = genericSuppTrRepository;
        }
        #region Chart
        public IEnumerable<PieDataDTO> GetFNDData(string Facility, string status, string sodata)
        {
            var parms = new[]
           {
                new SqlParameter("@Facility", Facility),
                new SqlParameter("@status", status),
                new SqlParameter("@SOList", string.IsNullOrWhiteSpace(sodata) ? DBNull.Value : (object)sodata)
            };
            return _db.Database.SqlQuery<PieDataDTO>($"SP_GetFNDData @Facility,@status,@SOList", parms);
        }
        public IEnumerable<PieDataDTO> GetMixionData(string Facility, string status, string sodata)
        {
            var parms = new[]
           {
                new SqlParameter("@Facility", Facility),
                new SqlParameter("@status", status),
                new SqlParameter("@SOList", string.IsNullOrWhiteSpace(sodata) ? DBNull.Value : (object)sodata)
            };
            return _db.Database.SqlQuery<PieDataDTO>($"SP_GetMIXIONData @Facility,@status,@SOList", parms);
        }
        public IEnumerable<PieDataDTO> GetGLData(string Facility, string status, string sodata)
        {
            var parms = new[]
           {
                new SqlParameter("@Facility", Facility),
                new SqlParameter("@status", status),
                new SqlParameter("@SOList", string.IsNullOrWhiteSpace(sodata) ? DBNull.Value : (object)sodata)
            };
            return _db.Database.SqlQuery<PieDataDTO>($"SP_GetGLData @Facility,@status,@SOList", parms);
        }
        public IEnumerable<PieDataDTO> GetExportData(string Facility, string status, string sodata)
        {
            var parms = new[]
           {
                new SqlParameter("@Facility", Facility),
                new SqlParameter("@status", status),
                new SqlParameter("@SOList", string.IsNullOrWhiteSpace(sodata) ? DBNull.Value : (object)sodata)
            };
            return _db.Database.SqlQuery<PieDataDTO>($"SP_GetExportData @Facility,@status,@SOList", parms);
        }
        #endregion Chart
        public IEnumerable<UserMasters> ListSuppTrDTO()
        {
            return _db.Database.SqlQuery<UserMasters>($"SP_IndexSuppList");
        }
       
        public IEnumerable<UserMasters> FilterLikeSuppTrDTO(string search_UID, string search_UName)
        {
            var parms = new[]
            {
                new SqlParameter("@search_uid", search_UID),
                new SqlParameter("@search_uname", search_UName),
            };
            return _db.Database.SqlQuery<UserMasters>($"SP_FilterLikeSuppList @search_uid,@search_uname", parms);
        }
        public IEnumerable<PieDataDTO> GetPieData(string Facility, string status, string sodata)
        {
            var parms = new[]
           {
                new SqlParameter("@Facility", Facility),
                new SqlParameter("@status", status),
                new SqlParameter("@SOList", string.IsNullOrWhiteSpace(sodata) ? DBNull.Value : (object)sodata)
            };
            return _db.Database.SqlQuery<PieDataDTO>($"SP_GetPieData @Facility,@status,@SOList", parms);
        }
        public IEnumerable<PieDataDTO> GetOCHData(string Facility, string status, string sodata)
        {
            var parms = new[]
           {
                new SqlParameter("@Facility", Facility),
                new SqlParameter("@status", status),
                new SqlParameter("@SOList", string.IsNullOrWhiteSpace(sodata) ? DBNull.Value : (object)sodata)
            };
            return _db.Database.SqlQuery<PieDataDTO>($"SP_GetOCHData @Facility,@status,@SOList", parms);
        }
        public IEnumerable<PieDataDTO> GetDeptData(string Facility, string status, string Discipline, string sodata)
        {
            var parms = new[]
           {
                new SqlParameter("@Facility", Facility),
                new SqlParameter("@status", status),
                new SqlParameter("@Discipline", Discipline),
                new SqlParameter("@SOList", string.IsNullOrWhiteSpace(sodata) ? DBNull.Value : (object)sodata)

            };
            return _db.Database.SqlQuery<PieDataDTO>($"SP_GetDeptData @Facility,@status,@Discipline,@SOList", parms);
        }
    }
}
