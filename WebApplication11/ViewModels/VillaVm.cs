using System.ComponentModel.DataAnnotations;

namespace WebApplication11.ViewModels
{
    public class VillaVm
    {
        public int Id { get; set; }
        public double Price { get; set; }

        [MinLength(3)]
        public string Title { get; set; }
        public string ? ImgUrl { get; set; }

      public   IFormFile? File { get; set; }
    }

}
