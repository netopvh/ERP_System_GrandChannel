﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothResorting.Dtos
{
    public class EFileDto
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string RootPath { get; set; }

        public DateTime UploadDate { get; set; }

        public string UploadBy { get; set; }
    }
}