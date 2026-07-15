using SUP_BAL.IRepository;
using SUP_CORE.DTO;
using SUP_CORE.EFModel;
using SUP_DAL.EFContextProvider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;

namespace SUP_BAL.BAL
{
    public class VRFBAL
    {
        private readonly SupplierDbContext _db; 
        public VRFBAL(SupplierDbContext SupplrContext)
        {
            this._db = SupplrContext; 
        }
       
    }
}
