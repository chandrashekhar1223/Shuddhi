using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Shuddhi.Model
{
    public class OtherWaste
    {

        public int ID { get; set; }

        [Required]
        public int User_ID { get; set; }
        public string? Clothes { get; set; }
        public string? Shoes { get; set; }
        public string? Thermocol { get; set; }
        public string? Used_Cooking_Oil { get; set; }
        public string? Mixed_Waste { get; set; }

        public string? Other_Waste { get; set; }

        public IFormFile? Other_Waste_Photo { get; set; }

        public string? Other_Waste_Photo_Path { get; set; }

        public string? CreatedBy { get; set; } 

        public string? UpdatedBy { get; set; }
    }
}
