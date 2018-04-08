using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KS_StockMgmtSystem.Model.ViewModel;
using KS_StockMgmtSystem.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace KS_StockMgmtSystem.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Stock")]
    public class StockController : Controller
    {
        private readonly IStockDataService _stockDataService;
        private readonly IVersionDataService _versionDataService;

        public StockController(IStockDataService stockDataService, IVersionDataService versionDataService) : base()
        {
            _stockDataService = stockDataService;
            _versionDataService = versionDataService;
        }

        [HttpGet]
        [Route("VersionList")]
        public async Task<List<StockVersionViewModel>> GetVersionList(int Year)
        {
            return await _stockDataService.GetVersion(Year);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody]UploadStockViewModel model)
        {
            var new_version = await _versionDataService.Create();
            if (new_version != 0)
            {
                model.Version = new_version;
                var result = await _stockDataService.UploadStockData("", model);
                if (result == false)
                {
                    var del_version = _versionDataService.Delete(new_version);
                    return BadRequest(Json(new { version = 0 }));
                }
                else
                {
                    return Ok(Json(new { version = new_version }));
                }
            }
            return BadRequest(Json(new { version = 0 }));
        }

    }
}