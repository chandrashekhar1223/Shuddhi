using System.ComponentModel.DataAnnotations;

namespace Shuddhi.Model
{
    public class VendorRegistration
    {

        [Key]
        public int ID { get; set; }   // Auto Generated

        public string? VendorName { get; set; }

      
        public DateTime? DateOfBirth { get; set; }

       
        public string? Gender { get; set; }

      
        public long? MobileNumber { get; set; }

     
        public string? EmailID { get; set; }

        
        public string? Password { get; set; }

     
        public string? ConfirmPassword { get; set; }

       
        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }   // Optional

     
        public string? AreaName { get; set; }

      
        public string? CityName { get; set; }

   
        public string? District { get; set; }

        
        public int? PinCode { get; set; }

     
        public int? RecycleMainID { get; set; }

       
        public int? RecycleSubID { get; set; }

        public DateTime? RegisteredOn { get; set; } = DateTime.Now;

        public bool? VendorStatus { get; set; } = true;

        public IFormFile? BusinessLicence { get; set; }

        public string? BusinessLicenceFileLink { get; set; }

        public IFormFile? GSTCertificate { get; set; }
        public string? GSTCertificateFileLink { get; set; }

        public IFormFile? Waste_Handling_Certificate { get; set; }
        public string? Waste_Handling_CertificateFileLink { get; set; }
         public IFormFile? Address_Proof { get; set; }
        public string? Address_ProofFileLink { get; set; }
        
        public IFormFile? Bank_Details { get; set; }
        public string? Bank_DetailsFileLink { get; set; }



    }
}
