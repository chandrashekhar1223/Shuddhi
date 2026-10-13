using System.ComponentModel.DataAnnotations;

namespace Shuddhi.Model
{
    public class DryWaste
    {

        public int ID { get; set; }

        [Required]
        public int User_ID { get; set; }

        public string? Paper { get; set; }

        public string? Cardboard { get; set; }

         public string? Plastic { get; set; }

        public string? Metal { get; set; }

        public string? Glass { get; set; }

        public string? Packaging_Waste { get; set; }

        public string? Other_Dry_Waste { get; set; }

        public IFormFile? Dry_Waste_Photo { get; set; }

        public string? Dry_Waste_Photo_Path { get; set; }


        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }


    }
}
