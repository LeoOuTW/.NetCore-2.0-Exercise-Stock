using KS_StockMgmtSystem.Model.IRepositories;
using KS_StockMgmtSystem.Service.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using KS_StockMgmtSystem.Model.Entities;

namespace KS_StockMgmtSystem.Service
{
    public class VersionDataService : IVersionDataService
    {
        private readonly IVersionDataRepository _versionDataRepository;
        public VersionDataService(IVersionDataRepository versionDataRepository)
        {
            _versionDataRepository = versionDataRepository;
        }

        public async Task<int> Create()
        {
            int max_version = 1;
            var list = _versionDataRepository.GetAll();
            if (list.Any())
            {
                max_version = list.OrderByDescending(x => x.Version).Select(x => x.Version).First() + 1;
            }

            var entity = new VersionData();
            entity.Version = max_version;
            entity.CreateDate = DateTime.Now;
            entity.UpdateDate = DateTime.Now;
            entity.Status = Status.Enabled;
            entity.CreateUser = "admin";

            _versionDataRepository.Insert(entity);

            if (_versionDataRepository.Save() > 0)
                return max_version;

            return 0;

        }

        public async Task<bool> Delete(int Version)
        {
            if (Version != 0)
            {
                var id = _versionDataRepository.Where(x => x.Version == Version).Select(x => x.Id).First();
                _versionDataRepository.Delete(id);
                return _versionDataRepository.Save() > 0;
            }
            return false;
        }
    }
}
