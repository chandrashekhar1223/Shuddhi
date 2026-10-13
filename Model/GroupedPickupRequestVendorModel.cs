namespace Shuddhi.Model
{
    public class GroupedPickupRequestVendorModel
    {

        public int Society_ID { get; set; }
        public string? Society { get; set; }

        public List<GroupedWasteTypeModel> WasteTypes { get; set; } = new();
    }
}
