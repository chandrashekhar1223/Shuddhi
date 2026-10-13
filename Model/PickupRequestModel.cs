namespace Shuddhi.Model
{
    public class PickupRequestModel
    {
        public int UserId { get; set; }

        public string Society { get; set; }

        public string? Address { get; set; }

        public string? Pincode { get; set; }

        public string? Note { get; set; }

        public decimal? TotalEstimatedWaste { get; set; }

        public DateTime? PickupDateTime { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public List<PickupWasteModel> WasteTypes { get; set; }

        // public IFormFile? Photo_ { get; set; }

        public IFormFile? DryWaste_Photo { get; set; }

        public IFormFile? WetWaste_Photo { get; set; }

        public IFormFile? EWaste_Photo { get; set; }

        public IFormFile? OtherWaste_Photo { get; set; }

        public int? PickupRequestId { get; set; }

        public Boolean? Status { get; set; }

        public DateTime? CreatedDate { get; set; }


        public int SocietyId { get; set; }
    }
}
