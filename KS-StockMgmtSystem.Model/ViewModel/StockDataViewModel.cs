using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KS_StockMgmtSystem.Model.ViewModel
{
    class StockDataViewModel
    {
    }

    public class UploadStockViewModel
    {
        public int Version { get; set; }
        public List<UploadStockDataViewModel> StockDataList { get; set; }
    }

    public class UploadStockDataViewModel
    {
        [MaxLength(64)]
        public string Name { get; set; }

        public int StockNum { get; set; }

        public int ConfirmYear { get; set; }

        public int ConfirmMonth { get; set; }
    }

    public class StockYearViewModel
    {
        public int ConfirmYear { get; set; }
    }

    public class StockVersionViewModel
    {
        public int Version { get; set; }
    }

    public class StockDataYearMonthViewModel
    {
        public int Version { get; set; }
        public int ConfirmYear { get; set; }
        public string StockName { get; set; }
        public int Jan { get; set; }
        public int Feb { get; set; }
        public int Mar { get; set; }
        public int Apr { get; set; }
        public int May { get; set; }
        public int Jun { get; set; }
        public int Jul { get; set; }
        public int Aug { get; set; }
        public int Sep { get; set; }
        public int Oct { get; set; }
        public int Nov { get; set; }
        public int Dec { get; set; }

        public StockDataYearMonthViewModel()
        {
            Jan = 0;
            Feb = 0;
            Mar = 0;
            Apr = 0;
            May = 0;
            Jun = 0;
            May = 0;
            Jun = 0;
            Jul = 0;
            Aug = 0;
            Sep = 0;
            Oct = 0;
            Nov = 0;
            Dec = 0;
        }
    }

}
