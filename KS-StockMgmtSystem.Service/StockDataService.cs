using KS_StockMgmtSystem.Model.IRepositories;
using KS_StockMgmtSystem.Model.ViewModel;
using KS_StockMgmtSystem.Service.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using KS_StockMgmtSystem.Model.Entities;

namespace KS_StockMgmtSystem.Service
{
    public class StockDataService : IStockDataService
    {
        private readonly IStockDataRepository _stockDataRepository;
        public StockDataService(IStockDataRepository stockDataRepository)
        {
            _stockDataRepository = stockDataRepository;
        }

        public async Task<List<StockDataYearMonthViewModel>> GetStockData(int ConfirmYear, int Version)
        {
            List<StockDataYearMonthViewModel> resultList = new List<StockDataYearMonthViewModel>();

            var list = _stockDataRepository.Where(x => x.ConfirmYear == ConfirmYear && x.Version == Version);
            if (list.Any())
            {
                var resulta = list.OrderBy(x=>x.Name).ToLookup((a) => a.Name);
                foreach (var stockName in resulta)
                {
                    var item = new StockDataYearMonthViewModel()
                    {
                        Version = Version,
                        StockName = stockName.Key,
                        ConfirmYear = ConfirmYear,
                    };

                    foreach (var detail in stockName)
                    {
                        switch (detail.ConfirmMonth)
                        {
                            case 1:
                                item.Jan = detail.StockNum;
                                break;
                            case 2:
                                item.Feb = detail.StockNum;
                                break;
                            case 3:
                                item.Mar = detail.StockNum;
                                break;
                            case 4:
                                item.Apr = detail.StockNum;
                                break;
                            case 5:
                                item.May = detail.StockNum;
                                break;
                            case 6:
                                item.Jun = detail.StockNum;
                                break;
                            case 7:
                                item.Jul = detail.StockNum;
                                break;
                            case 8:
                                item.Aug = detail.StockNum;
                                break;
                            case 9:
                                item.Sep = detail.StockNum;
                                break;
                            case 10:
                                item.Oct = detail.StockNum;
                                break;
                            case 11:
                                item.Nov = detail.StockNum;
                                break;
                            case 12:
                                item.Dec = detail.StockNum;
                                break;
                        }
                    }

                    resultList.Add(item);
                }
            }
            return resultList;
        }

        public async Task<List<StockVersionViewModel>> GetVersion(int ConfirmYear)
        {
            var list = _stockDataRepository.Where(x => x.ConfirmYear == ConfirmYear);
            if (list.Any())
            {
                var result = list.GroupBy(x => x.Version).Select(x => x.First()).ToList();
                return result.OrderByDescending(x=>x.Version).Select(x => new StockVersionViewModel() {
                    Version = x.Version
                }).ToList();
            }
            return null;
        }

        public async Task<List<StockYearViewModel>> GetYear()
        {
            var list = _stockDataRepository.GetAll();
            if (list.Any())
            {
                var result = list.GroupBy(x => x.ConfirmYear).Select(x => x.First()).ToList();
                return result.OrderByDescending(x=>x.ConfirmYear).Select(x=>new StockYearViewModel() {
                    ConfirmYear = x.ConfirmYear
                }).ToList();
            }
            return null;
            
        }

        public async Task<bool> UploadStockData(string UserId, UploadStockViewModel Model)
        {
            foreach(var tmp in Model.StockDataList)
            {
                var entity = new StockData();
                entity.Name = tmp.Name;
                entity.ConfirmMonth = tmp.ConfirmMonth;
                entity.ConfirmYear = tmp.ConfirmYear;
                entity.CreateDate = DateTime.Now;
                entity.CreateUser = UserId;
                entity.Name = tmp.Name;
                entity.Status = Status.Enabled;
                entity.StockNum = tmp.StockNum;
                entity.UpdateDate = DateTime.Now;
                entity.Version = Model.Version;

                _stockDataRepository.Insert(entity);

            }
            return _stockDataRepository.Save() > 0;
        }


    }
}
