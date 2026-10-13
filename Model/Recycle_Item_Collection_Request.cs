namespace Shuddhi.Model
{
    public class Recycle_Item_Collection_Request
    {
        public string Id { get; set; }

        public DateTime RequestDate { get; set; }

        public string RequesterId { get; set; }

        public string RecycleMainId { get; set; }

        public string RecycleSubId { get; set; }

        public byte[] RecycleItemPhoto { get; set; }

        public int RecycleItemWeightByRequester { get; set; }

        public string VendorAssignedStatus { get; set; }

        public string VendorId { get; set; }

        public string VendorAssignedBy { get; set; }

        public DateTime VendorAssignedOn { get; set; }

        public string RecycleItemWeightStatus { get; set; }

        public int? RecycleItemWeightByVendor { get; set; }

        public string RecycleItemCollectedStatus { get; set; }

        public DateTime RecycleItemCollectedOn { get; set; }

        public string RecycleItemWeightVariation { get; set; }

        public string RecycleItemWeightIssue { get; set; }

        public string RequestAcceptedStatus { get; set; }

        public DateTime RequesterAcceptedOn { get; set; }

        public int RedeemPoints { get; set; }

        public string RedeemPointsAssignedBy { get; set; }

        public DateTime RedeemPointsAssignedOn { get; set; }
    }

}
