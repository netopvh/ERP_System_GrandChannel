﻿using ClothResorting.Dtos;
using ClothResorting.Helpers;
using ClothResorting.Models;
using ClothResorting.Models.DataTransferModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Data.Entity;
using AutoMapper;

namespace ClothResorting.Controllers.Api
{
    // To DO: 此api代码需要重构
    public class RereivingDetailExcelFileController : ApiController
    {
        private ApplicationDbContext _context;

        public RereivingDetailExcelFileController()
        {
            _context = new ApplicationDbContext();
        }

        // POST /api/RetrievingDetailExcelFile
        [HttpPost]
        public IHttpActionResult GetRetrievingRecord()
        {
            var fileSavePath = "";
            var pickRequestList = new List<PickRequest>();

            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var httpPostedFile = HttpContext.Current.Request.Files[0];

                if (httpPostedFile != null)
                {
                    fileSavePath = @"D:\TempFiles\" + httpPostedFile.FileName;

                    httpPostedFile.SaveAs(fileSavePath);
                }
            }

            var excel = new LoadPlanExtracter(fileSavePath);
            
            pickRequestList.AddRange(excel.GetPickRequestsFromXlsx());

            //输入pickRequestList，输出retrievingRecords
            var result = new List<RetrievingRecordDto>();
            var po = pickRequestList[0].PurchaseOrder;
            var count = 0;      //记录本次取货生成多少条记录
            var loadPlan = new LoadPlanRecord
            {
                PurchaseOrder = po,
                OutBoundDate = DateTime.Now,
                OutBoundPcs = 0,
                OutBoundCtns = 0
            };

            _context.LoadPlanRecords.Add(loadPlan);
            _context.SaveChanges();

            var cartonBreakdowns = _context.CartonBreakDowns
                .Include(c => c.CartonDetail)
                .Include(c => c.PackingList.PreReceiveOrder)
                .Where(c => c.PurchaseOrder == po
                    && c.AvailablePcs != 0
                    && c.RunCode == "");

            var loadPlanInDb = _context.LoadPlanRecords
                .OrderByDescending(c => c.Id).First();

            foreach(var request in pickRequestList)
            {
                //分别按照style、color、size筛选
                var cartonWithRightStyle = cartonBreakdowns.Where(c => c.Style == request.Style);
                var cartonWithRightColor = cartonWithRightStyle.Where(c => c.Color == request.Color);
                var cartonBreakdownResults = cartonWithRightColor.Where(c => c.Size == request.Size).ToList();

                if(cartonBreakdownResults.Count != 0)
                {
                    var calculator = new CartonsCalculator();

                    //调用Helper中CartonsCalculator的方法
                    var query = calculator.CalculateCartons(cartonBreakdownResults, request.TotalPcs, loadPlanInDb).ToList();

                    _context.RetrievingRecords.AddRange(query);
                    _context.SaveChanges();

                    count += query.Count;
                }
            }

            var q = _context.RetrievingRecords
                .OrderByDescending(r => r.Id)
                .Select(Mapper.Map<RetrievingRecord, RetrievingRecordDto>)
                .Take(count)
                .OrderBy(r => r.Id);

            //强行释放EXCEL资源(终止EXCEL进程)
            excel.Dispose();

            return Created(Request.RequestUri + "/" + 111, q);
        }
    }
}
