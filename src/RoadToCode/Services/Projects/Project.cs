﻿using System;
using System.Collections.Generic;
using RoadToCode.Models;

namespace RoadToCode.Services.Projects
{
    public class Project : IDatabaseModel
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string HtmlContent { get; set; }
        public ModelState State { get; set; }
        public string Id { get; set; }
        public IList<DateTime> Updated { get; set; }
    }
}