using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MyWebpage.Abstract
{
    public interface IBlogPosts
    {
        List<IBlogPost> BlogPosts { get; set; }
        void RemoveById(int id);
    }
}