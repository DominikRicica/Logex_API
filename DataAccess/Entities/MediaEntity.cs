﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class MediaEntity
    {
        public int MediaId { get; set; }
        public int ItemId { get; set; }
        public string Url { get; set; }
    }
}
