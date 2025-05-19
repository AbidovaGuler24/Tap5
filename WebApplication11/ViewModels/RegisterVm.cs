using System.ComponentModel.DataAnnotations;

namespace WebApplication11.ViewModels
{
    public class RegisterVm
    {
        [MinLength(3)]
        public string Name { get; set; }
        [MinLength(3)]
        public string Surname { get; set; } 

        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Email { get; set; }
        [DataType(DataType.Password),Compare("ConfirmPassword")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
