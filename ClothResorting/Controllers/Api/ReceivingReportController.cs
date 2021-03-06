﻿using ClothResorting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using AutoMapper;
using ClothResorting.Dtos;
using ClothResorting.Models.ApiTransformModels;
using System.Web;

namespace ClothResorting.Controllers.Api
{
    public class ReceivingReportController : ApiController
    {
        private ApplicationDbContext _context;
        private string _userName;

        public ReceivingReportController()
        {
            _context = new ApplicationDbContext();
            _userName = HttpContext.Current.User.Identity.Name.Split('@')[0];
        }

        // GET /receivingreport/?preid={preId}&container={container}
        [HttpGet]
        public IHttpActionResult GetAllCartonDetails([FromUri]int preid, [FromUri]string container)
        {
            var cartonDetails = _context.RegularCartonDetails
                .Include(c => c.POSummary.PreReceiveOrder)
                .Where(c => c.POSummary.PreReceiveOrder.Id == preid
                    && c.POSummary.Container == container)
                .Select(Mapper.Map<RegularCartonDetail, RegularCartonDetailDto>)
                .ToList();

            return Ok(cartonDetails);
        }

        // PUT /receivingreport/
        [HttpPut]
        public void UpdateComment([FromBody]PreIdCommentJsonObj obj)
        {
            var cartonDetailInDb = _context.RegularCartonDetails.Find(obj.Id);

            cartonDetailInDb.Comment = obj.Comment;
            cartonDetailInDb.Adjustor = _userName;

            _context.SaveChanges();
        }
    }
}
