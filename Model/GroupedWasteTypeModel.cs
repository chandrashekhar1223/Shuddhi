namespace Shuddhi.Model
{
    public class GroupedWasteTypeModel
    {

        public int WasteTypeId { get; set; }

        public List<PickupRequestVendorModel> PickupRequests { get; set; } = new();
    }
}
