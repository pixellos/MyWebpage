using System;
using System.Collections;
using System.Collections.Generic;
using Pixel.Results;

namespace RoadToCode.Services.Blog
{
    public interface IPostProvider : IEnumerable<Post>
    {
        IResult Add(Post post);
        IResult Hide(Post post);
        IResult Edit(Post post);
        IResult<Post> NewestByTitle(string title);
    }
}