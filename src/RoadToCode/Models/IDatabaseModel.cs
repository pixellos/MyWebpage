using System;
using System.Collections.Generic;

namespace RoadToCode.Models
{
    public interface IDatabaseModel
    {
        string Id { get; set; }
        IList<DateTime> Updated { get; set; }
        ModelState State{ get; set; }
    }
}