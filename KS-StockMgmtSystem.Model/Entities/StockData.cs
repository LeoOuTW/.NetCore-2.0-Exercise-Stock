using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KS_StockMgmtSystem.Model.Entities
{
    public class StockData : EntityIntWithRecord
    {
        public int Version { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }

        public int StockNum { get; set; }

        public int ConfirmYear { get; set; }

        public int ConfirmMonth { get; set; } 

    }
}
