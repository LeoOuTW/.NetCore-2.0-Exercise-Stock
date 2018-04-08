using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KS_StockMgmtSystem.Model.ViewModel;
using KS_StockMgmtSystem.Model.ViewModel.FrontEnd;
using KS_StockMgmtSystem.Service.IService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using X.PagedList;

namespace KS_StockMgmtSystem.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockDataService _stockDataService;
        private IHostingEnvironment _hostingEnvironment;

        private readonly int pageSize = 10;

        public StockController(IStockDataService stockDataService, IHostingEnvironment hostingEnvironment) : base()
        {
            _stockDataService = stockDataService;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var YearList =  await _stockDataService.GetYear();
            SelectList YearSelectList = new SelectList(YearList, "ConfirmYear", "ConfirmYear");

            ViewBag.YearDDL = YearSelectList;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List_Partial(int Year, int Version, int? Page = 1)
        {
            //StockDataSearchResultListViewModel resultModel = new StockDataSearchResultListViewModel();
            var list = await _stockDataService.GetStockData(Year, Version);
            var onePageOfList = list.ToPagedList(Page.Value, pageSize);
            //resultModel.List = list;
            //ViewData.Model = resultModel;
            ViewBag.OnePageOfList = onePageOfList;
            ViewBag.Year = Year;
            ViewBag.Version = Version;

            return PartialView("_List_Partial");
        }

        [HttpPost]
        public async Task<IActionResult> List(int ddl_Year, int ddl_Version)
        {
            var list = await _stockDataService.GetStockData(ddl_Year, ddl_Version);

            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"Stock_" + ddl_Year.ToString() + "_" + ddl_Version + ".xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Demo");
                IRow row = excelSheet.CreateRow(0);

                row.CreateCell(0).SetCellValue("Version");
                row.CreateCell(1).SetCellValue("Year");
                row.CreateCell(2).SetCellValue("Item");
                row.CreateCell(3).SetCellValue("Jan");
                row.CreateCell(4).SetCellValue("Feb");
                row.CreateCell(5).SetCellValue("Mar");
                row.CreateCell(6).SetCellValue("Apr");
                row.CreateCell(7).SetCellValue("May");
                row.CreateCell(8).SetCellValue("Jun");
                row.CreateCell(9).SetCellValue("Jul");
                row.CreateCell(10).SetCellValue("Aug");
                row.CreateCell(11).SetCellValue("Sep");
                row.CreateCell(12).SetCellValue("Oct");
                row.CreateCell(13).SetCellValue("Nov");
                row.CreateCell(14).SetCellValue("Dec");

                int i = 1;
                foreach(var tmp in list)
                {
                    row = excelSheet.CreateRow(i);

                    row.CreateCell(0).SetCellValue(tmp.Version);
                    row.CreateCell(1).SetCellValue(tmp.ConfirmYear);
                    row.CreateCell(2).SetCellValue(tmp.StockName);
                    row.CreateCell(3).SetCellValue(tmp.Jan.ToString());
                    row.CreateCell(4).SetCellValue(tmp.Feb.ToString());
                    row.CreateCell(5).SetCellValue(tmp.Mar.ToString());
                    row.CreateCell(6).SetCellValue(tmp.Apr.ToString());
                    row.CreateCell(7).SetCellValue(tmp.May.ToString());
                    row.CreateCell(8).SetCellValue(tmp.Jun.ToString());
                    row.CreateCell(9).SetCellValue(tmp.Jul.ToString());
                    row.CreateCell(10).SetCellValue(tmp.Aug.ToString());
                    row.CreateCell(11).SetCellValue(tmp.Sep.ToString());
                    row.CreateCell(12).SetCellValue(tmp.Oct.ToString());
                    row.CreateCell(13).SetCellValue(tmp.Nov.ToString());
                    row.CreateCell(14).SetCellValue(tmp.Dec.ToString());

                    i++;
                }

                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }
    }
}