using KS_StockMgmtSystem.Model.Entities;
using KS_StockMgmtSystem.Model.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace KS_StockMgmtSystem.Model.Repositories
{
    public class StockDataRepository : SystemRepositoryBaseInt<StockData>, IStockDataRepository
    {
        public StockDataRepository(SystemDBContext dbcontext) : base(dbcontext)
        {
        }
    }
}
