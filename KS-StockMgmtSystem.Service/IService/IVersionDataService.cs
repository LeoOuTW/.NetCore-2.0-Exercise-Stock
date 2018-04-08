using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KS_StockMgmtSystem.Service.IService
{
    public interface IVersionDataService
    {
        Task<bool> Delete(int Version);
        Task<int> Create();
    }
}
