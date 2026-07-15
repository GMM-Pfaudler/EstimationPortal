using SUP_BAL.IRepository;
using SUP_CORE.DTO;
using SUP_CORE.EFModel;
using SUP_DAL.EFContextProvider;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;

namespace SUP_BAL.BAL
{
    public class PODetailsBAL
    {
        private readonly SupplierDbContext _db; 
        public PODetailsBAL(SupplierDbContext SupplrContext)
        {
            this._db = SupplrContext; 
        }
        public List<DocItemDTO> CorrectStatusList()
        {
           
            //return _db.Database.SqlQuery<DocItemDTO>($"SP_CorrectStatusList").ToList();
            return _db.Database.SqlQuery<DocItemDTO>($"SP_GetCorrectStatusList").ToList();
        }
        public IEnumerable<QLT_PODTO> FilterPONoListTRDTO(string search, string SUserId, string RoleID)
        {
            var parms = new[]
            {
                new SqlParameter("@search", search),
                new SqlParameter("@SUserId", SUserId),
                new SqlParameter("@RoleID", RoleID),
            };
            return _db.Database.SqlQuery<QLT_PODTO>($"SP_QLT_FilterLikePONoList @search, @SUserId, @RoleID", parms);
        }

        public IEnumerable<QLT_PODTO> FilterQLTPOList(string po)
        {
            var parms = new[]
            { 
                new SqlParameter("@PO", po),
            };
            return _db.Database.SqlQuery<QLT_PODTO>($"SP_QLT_FilterOfficeList @PO", parms);
        }
        public IEnumerable<QLT_PODTO> QLTiInspList()
        {
            return _db.Database.SqlQuery<QLT_PODTO>($"SP_QLTInspListData");
        }
        public IEnumerable<QLT_PODTO> QLTiInspList(string po)
        {
            //return _db.Database.SqlQuery<QLT_PODTO>($"SP_QLTInspListData").Where(s=>s.Office == po); 
            return _db.Database.SqlQuery<QLT_PODTO>($"SP_QLTInspListData");
        }
        public IEnumerable<QLT_PODTO> QLTiInspEngList(int? EngId)
        {
             var parms = new[]
            {
                new SqlParameter("@EngId", EngId)
            };
            return _db.Database.SqlQuery<QLT_PODTO>($"SP_QLTInspEngListData @EngId", parms);
        }
        public IEnumerable<QLT_PODTO> FilterDateWiseList(DateTime? min, DateTime? max, int? EngId)
        {
            var parms = new[]
            {
                new SqlParameter("@min", min),
                new SqlParameter("@max", max),
                new SqlParameter("@EngId", EngId)
            };
            return _db.Database.SqlQuery<QLT_PODTO>($"SP_QLT_FilterQLTList @min,@max,@EngId", parms);
        }
        public IEnumerable<QLT_PODTO> FilterPOTrList(DateTime? min, DateTime? max)
        {
            var parms = new[]
            {
                new SqlParameter("@min", min),
                new SqlParameter("@max", max),
            };
            return _db.Database.SqlQuery<QLT_PODTO>($"SP_FilterPODetailsList @min,@max", parms);
        }
        public IEnumerable<QLT_PODTO> FilterPOTrList1()
        {
            return _db.Database.SqlQuery<QLT_PODTO>($"SP_FilterPODetailsList1");
        }

        public IEnumerable<QLT_PODTO> FilterPOTrListPlanning(string Office)
        {
            return _db.Database.SqlQuery<QLT_PODTO>("SP_FilterPOTrListPlanning @Office", new SqlParameter("Office", Office));
        }
        public IEnumerable<QLT_PODTO> FilterPOTrListByMonYear(DateTime? min, DateTime? max, string office)
        {
            var parms = new[]
            {
                new SqlParameter("@min", min),
                new SqlParameter("@max", max),
                new SqlParameter("@office", office),
            };
            return _db.Database.SqlQuery<QLT_PODTO>("SP_FilterPOTrListByMonYear @min,@max,@office", parms);
        }

         

        public IEnumerable<RevHistoryDTO> RevTransactionList(int? id)
        {
            var parms = new[]
           {
                new SqlParameter("@id", id),
            };
            return _db.Database.SqlQuery<RevHistoryDTO>($"SP_RevHistory @id", parms);
        }
        
    }
}
