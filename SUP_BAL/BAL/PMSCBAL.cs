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
    public class PMSCBAL
    {
        private readonly SupplierDbContext _db;
        public PMSCBAL(SupplierDbContext SupplrContext)
        {
            this._db = SupplrContext;
        }
        

    }
}
