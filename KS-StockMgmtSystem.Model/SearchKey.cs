using KS_StockMgmtSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KS_StockMgmtSystem.Model
{
    public class SearchKey
    {
        public string Title { get; set; }
        public Status? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
