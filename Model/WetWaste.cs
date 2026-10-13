using System.ComponentModel.DataAnnotations;

namespace Shuddhi.Model
{
    public class WetWaste
    {

        public int ID { get; set; }

        [Required]
        public int User_ID { get; set; }
        public string? Food_Waste { get; set; }

        public string? Vegetable_Waste { get; set; }

        public string? Fruit_Waste { get; set; }

        public string? Garden_Waste { get; set; }

        public string? Organic_Waste { get; set; }

        public string? Other_Wet_Waste { get; set; }

        public IFormFile? Wet_Waste_Photo { get; set; }

        public string? Wet_Waste_Photo_Path { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
