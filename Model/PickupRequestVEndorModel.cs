namespace Shuddhi.Model
{
    public class PickupRequestVendorModel
    {
        public int? PickupRequestId { get; set; }
        public int? UserId { get; set; }
        public string? Address { get; set; }
        public string? Note { get; set; }
        public decimal? TotalEstimatedWaste { get; set; }
        public DateTime? PickupDateTime { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Status { get; set; }
        public string? Pincode { get; set; }
        public int? Assigned_Vendor_Id { get; set; }

        public int? PickupRequestDetailId { get; set; }
        public int? WasteTypeId { get; set; }
        // public int WasteSubTypeId { get; set; }

        public string? WasteSubTypeId { get; set; }
        public decimal? Quantity { get; set; }
        public string? PhotoPath { get; set; }
        public DateTime? DetailCreatedDate { get; set; }

        public string? Society { get; set; }

        public int? ID { get; set; }
    }
}
