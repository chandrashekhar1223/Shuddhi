using System.ComponentModel.DataAnnotations;

namespace Shuddhi.Model
{
    public class UserRegistration
    {
        [Key]
        public int ID { get; set; }   // Auto

        [Required]
        public string? FullName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

      
        public string? Gender { get; set; }

       
        public long? MobileNumber { get; set; }

      
        public string? EmailID { get; set; }

        
        public string? Password { get; set; }

        
        
        public string? ConfirmPassword { get; set; }

       
        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }   // Optional

      
        public string? SocietyName { get; set; }

       
        public string? AreaName { get; set; }

       
        public string? CityName { get; set; }

     
        public string? District { get; set; }

      
        public int? PinCode { get; set; }

        public DateTime? RegisteredOn { get; set; } = DateTime.Now;

        public bool? UserStatus { get; set; }

        public IFormFile? User_Profile_Photo { get; set; }

        public string? User_Profile_photo_url { get; set; }


        public String? Wing { get; set; }


        public string? Sector { get; set; }

        public string? Landmark { get; set; }

        public string? Plot_No { get; set; }

        public string? House_No { get; set; }

        public string? FLoor { get;set; }


        public int? Society_Id { get; set; }
       


    }
}
