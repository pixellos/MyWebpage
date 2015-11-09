using System;
using System.ComponentModel.DataAnnotations;

namespace MyWebpage.Abstract
{
    public interface IBlogPost
    {
        [Key]
        int Id { get; set; }

        DateTime BlogDateTime { get; set; }
        string Content { get; set; }
        string Header { get; set; }
        string PhotoAdress { get; set; }
        string PhotoDescribtion { get; set; }
    }
}