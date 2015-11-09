using System.ComponentModel.DataAnnotations;

namespace MyWebpage.Abstract
{
    public interface IArticleSheet
    {
        [Key]
        int Id { get; set; }

        string Headline { get; set; }
        string Description { get; set; }
        string PhotoShowAdress { get; set; }
        string Article { get; set; }
    }
}