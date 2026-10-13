using System.ComponentModel.DataAnnotations;

namespace Shuddhi.Model
{
    public class EWaste
    {

        public int ID { get; set; }

        [Required]
        public int User_ID { get; set; } 
        public string? Mobile { get; set; }
        public string? Batteries { get; set; }
        public string? Charger { get; set; }
        public string? Laptop { get; set; }
        public string? Cable { get; set; }

        public string? Small_Appliances { get; set; }

        public string? Electrical_Equipment { get; set; }

        public string? Electronic_Equipment { get; set; }

        public string? Other_E_Waste { get; set; }

        public IFormFile? E_Waste_Photo { get; set; }

        public string? E_Waste_Photo_Path { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
