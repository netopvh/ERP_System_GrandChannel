﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothResorting.Models
{
    public class RegularLocationDetail
    {
        public int Id { get; set; }

        public string PurchaseOrder { get; set; }

        public string Style { get; set; }

        public string Color { get; set; }

        public string RunCode { get; set; }

        public int OrgNumberOfCartons { get; set; }

        public int InvNumberOfCartons { get; set; }

        public int OrgPcs { get; set; }

        public int InvPcs { get; set; }

        public string Location { get; set; }

        public DateTime InboundDate { get; set; }

        public PurchaseOrderSummary PurchaseOrderSummary { get; set; }
    }
}