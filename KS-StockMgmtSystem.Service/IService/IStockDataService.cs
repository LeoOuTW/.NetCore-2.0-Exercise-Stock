using KS_StockMgmtSystem.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KS_StockMgmtSystem.Service.IService
{
    public interface IStockDataService
    {
        Task<bool> UploadStockData(string UserId, UploadStockViewModel Model);
        Task<List<StockYearViewModel>> GetYear();
        Task<List<StockVersionViewModel>> GetVersion(int ConfirmYear);
        Task<List<StockDataYearMonthViewModel>> GetStockData(int ConfirmYear, int Version);
    }
}
