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

namespace SUP_BAL.BAL
{
    public class InvoiceBAL
    {
        private readonly SupplierDbContext _db;
        private readonly IGenericRepository<InvoiceDetails> _genericINVTrRepository;
        public InvoiceBAL(SupplierDbContext SupplrContext, IGenericRepository<InvoiceDetails> genericINVTrRepository)
        {
            this._db = SupplrContext;
            _genericINVTrRepository = genericINVTrRepository;
        }
        public IEnumerable<InvoiceDetails> ListINVTrDTO()
        {
            return _db.Database.SqlQuery<InvoiceDetails>($"SP_IndexInvoicesList");
        }
        
        public IEnumerable<InvoiceDetails> FilterLikeListINVTrDTO(
              dynamic search_Invoicedate,
                   string search_BPCode = "",
                string search_BPName = "",
                string search_OrderNo = "",
                string search_InvoiceNo = "",
                string search_TransactionType = "",
                string search_GMMRef = "")
        {
            var parms = new[]
            {
                new SqlParameter("@invoiceDt", search_Invoicedate),
                new SqlParameter("@bpCode", search_BPCode),
                new SqlParameter("@bpName", search_BPName),
                new SqlParameter("@orderNo", search_OrderNo),
                new SqlParameter("@invoiceNo", search_InvoiceNo),
                new SqlParameter("@tranType", search_TransactionType),
                new SqlParameter("@gmmRef", search_GMMRef),
            };
            return _db.Database.SqlQuery<InvoiceDetails>($"SP_FilterLikeInvoicesList @invoiceDt,@bpCode,@bpName,@orderNo,@invoiceNo,@tranType,@gmmRef", parms);
        }
        public IEnumerable<InvoiceDetails> FilterINVTrList(DateTime? min, DateTime? max)
        {
            var parms = new[]
            {
                new SqlParameter("@min", min),
                new SqlParameter("@max", max),

            };
            return _db.Database.SqlQuery<InvoiceDetails>($"SP_FilterInvoicesList @min,@max", parms);
        }
    }
}
