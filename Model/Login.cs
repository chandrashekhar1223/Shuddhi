using System.ComponentModel.DataAnnotations;

namespace Shuddhi.Model
{
    public class Login
    {
        [Required(ErrorMessage = "User Id is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }


        public int? IsValidUser { get; set; }

        public string? Mail_Id { get; set; }

        public string? Flag { get; set; }

      



    }
}
