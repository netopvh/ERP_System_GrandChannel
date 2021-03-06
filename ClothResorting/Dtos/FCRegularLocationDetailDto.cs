﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothResorting.Dtos
{
    public class FCRegularLocationDetailDto
    {
        public int Id { get; set; }

        public string Container { get; set; }

        public string PurchaseOrder { get; set; }

        public string Style { get; set; }

        public string Color { get; set; }

        public string CustomerCode { get; set; }

        public string SizeBundle { get; set; }

        public string PcsBundle { get; set; }

        public int Cartons { get; set; }

        public int Quantity { get; set; }

        public int PcsPerCaron { get; set; }

        public string Status { get; set; }

        public string Location { get; set; }

        public int AvailableCtns { get; set; }

        public int PickingCtns { get; set; }

        public int ShippedCtns { get; set; }

        public int AvailablePcs { get; set; }

        public int PickingPcs { get; set; }

        public int ShippedPcs { get; set; }

        public DateTime InboundDate { get; set; }

        public string CartonRange { get; set; }

        public string Allocator { get; set; }

        public string Vendor { get; set; }

        public string Batch { get; set; }

        public string ColorCode { get; set; }
    }
}