using WebApplication11.Models.Base;

namespace WebApplication11.Models
{
    public class Villa:BaseEntity
    {
        public double Price { get; set; }

        public string ImgUrl { get; set; }
        public string Title { get; set; }

       

    }
}
