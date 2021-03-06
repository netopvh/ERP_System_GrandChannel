﻿using ClothResorting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClothResorting.Controllers.Api
{
    public class InventorySearchController : ApiController
    {
        private ApplicationDbContext _context;

        public InventorySearchController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/inventorysearch/?vendor={vendor}&container={container}&purchaseOrder={po}&style={style}&color={color}&customer={customer}&size={size}&location={location}&isShipped={isShipped}&isReplenishment={isReplenishment}
        [HttpGet]
        public IHttpActionResult SearchInInventory([FromUri]string vendor, [FromUri]string container, [FromUri]string purchaseOrder, [FromUri]string style, [FromUri]string color, [FromUri]string customer, [FromUri]string size, [FromUri]string location, [FromUri]bool isShipped, [FromUri]bool isReplenishment)
        {

            if (isReplenishment)
            {
                var replenishmentLocationDetails = _context.ReplenishmentLocationDetails.Where(x => x.Vendor == vendor).ToList();
                var replenishmentResult = new List<InventoryReportDetail>();

                if (purchaseOrder != "NULL")
                {
                    replenishmentLocationDetails = replenishmentLocationDetails.Where(x => x.PurchaseOrder == purchaseOrder).ToList();
                }

                if (style != "NULL")
                {
                    replenishmentLocationDetails = replenishmentLocationDetails.Where(x => x.Style == style).ToList();
                }

                if (color != "NULL")
                {
                    replenishmentLocationDetails = replenishmentLocationDetails.Where(x => x.Color == color).ToList();
                }

                if (size != "NULL")
                {
                    replenishmentLocationDetails = replenishmentLocationDetails.Where(x => x.Size == size).ToList();
                }

                if (location != "NULL")
                {
                    replenishmentLocationDetails = replenishmentLocationDetails.Where(x => x.Location == location).ToList();
                }

                if (!isShipped)
                {
                    replenishmentLocationDetails = replenishmentLocationDetails.Where(x => x.AvailablePcs != 0).ToList();
                }

                foreach(var rep in replenishmentLocationDetails)
                {
                    replenishmentResult.Add(new InventoryReportDetail {
                        Vendor = rep.Vendor,
                        Status = rep.Status,
                        PurchaseOrder = rep.PurchaseOrder,
                        Style = rep.Style,
                        Color = rep.Color,
                        SizeBundle = rep.Size,
                        Cartons = rep.Cartons,
                        Quantity = rep.Quantity,
                        AvailableCtns = rep.AvailableCtns,
                        PickingCtns = rep.PickingCtns,
                        ShippedCtns = rep.ShippedCtns,
                        AvailablePcs = rep.AvailablePcs,
                        PickingPcs = rep.PickingPcs,
                        ShippedPcs = rep.ShippedPcs,
                        InboundDate = rep.InboundDate,
                        Location = rep.Location
                    });
                }

                return Ok(replenishmentResult);
            }
            else
            {
                var locationDetails = _context.FCRegularLocationDetails.Where(x => x.Vendor == vendor).ToList();

                if (container != "NULL")
                {
                    locationDetails = locationDetails.Where(x => x.Container.Contains(container)).ToList();
                }

                if (purchaseOrder != "NULL")
                {
                    locationDetails = locationDetails.Where(x => x.PurchaseOrder.Contains(purchaseOrder)).ToList();
                }

                if (style != "NULL")
                {
                    locationDetails = locationDetails.Where(x => x.Style.Contains(style)).ToList();
                }

                if (color != "NULL")
                {
                    locationDetails = locationDetails.Where(x => x.Color.Contains(color)).ToList();
                }

                if (customer != "NULL")
                {
                    locationDetails = locationDetails.Where(x => x.CustomerCode == customer).ToList();
                }

                if (size != "NULL")
                {
                    locationDetails = locationDetails.Where(x => x.SizeBundle == size).ToList();
                }

                if (location != "NULL")
                {
                    locationDetails = locationDetails.Where(x => x.Location == location).ToList();
                }

                if (!isShipped)
                {
                    locationDetails = locationDetails.Where(x => x.AvailablePcs != 0).ToList();
                }

                return Ok(locationDetails);
            }
        }
    }
}

public class InventoryReportDetail
{
    public int Id { get; set; }

    public string CustomerCode { get; set; }

    public string Container { get; set; }

    public string PurchaseOrder { get; set; }

    public string Style { get; set; }

    public string Color { get; set; }

    public string SizeBundle { get; set; }

    public int Cartons { get; set; }

    public string Pack { get; set; }

    public int AvailableCtns { get; set; }

    public int PickingCtns { get; set; }

    public int ShippedCtns { get; set; }

    public int Quantity { get; set; }

    public int AvailablePcs { get; set; }

    public int PickingPcs { get; set; }

    public int ShippedPcs { get; set; }

    public string Location { get; set; }

    public string Operator { get; set; }

    public string Editor { get; set; }

    public string Status { get; set; }

    public DateTime InboundDate { get; set; }

    public string Vendor { get; set; }

    public bool IsHanger { get; set; }

    public string Batch { get; set; }
}