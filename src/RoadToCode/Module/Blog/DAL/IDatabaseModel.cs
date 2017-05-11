using System;

namespace RoadToCode.Module.Blog.DAL
{
    public interface IDatabaseModel
    {
        int Id { get; set; }
        DateTime DateTime { get; set; }
    }
}