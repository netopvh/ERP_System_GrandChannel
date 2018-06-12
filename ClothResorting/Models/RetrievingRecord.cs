﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothResorting.Models
{
    public class RetrievingRecord
    {
        public int Id { get; set; }

        public string Location { get; set; }

        public string PurchaseOrderNumber { get; set; }

        public string Style { get; set; }

        public string Color { get; set; }

        public string Size { get; set; }

        public int? RetrivedPcs { get; set; }

        public int? TargetPcs { get; set; }

        public int? NumberOfCartons { get; set; }

        public int? TotalOfCartons { get; set; }

        public bool IsOpened { get; set; }

        public int ShoulReturnPcs { get; set; }

        public DateTime? RetrievedDate { get; set; }

        public LoadPlanRecord LoadPlanRecord { get; set; }
    }
}