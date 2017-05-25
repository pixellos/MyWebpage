using System;
using System.Collections;
using System.Collections.Generic;
using RoadToCode.Models.Results;

namespace RoadToCode.Services.Blog
{
    public interface IPostProvider : IEnumerable<Post>
    {
        Result Add(Post post);
        Result Edit(Post post);
    }
}