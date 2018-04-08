using System;
using System.Collections.Generic;
using System.Text;

namespace KS_StockMgmtSystem.Model.ViewModel.FrontEnd
{
    public class StockDataSearchResultListViewModel
    {
        public List<StockDataYearMonthViewModel> List { get; set; }
    }

    public class StockDataSearchDataViewModel
    {

        public List<StockYearViewModel> YearList { get; set; }

    }
}
