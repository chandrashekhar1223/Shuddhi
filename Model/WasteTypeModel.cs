namespace Shuddhi.Model
{
    public class WasteTypeModel
    {
        public int WasteTypeId { get; set; }

        public string WasteType { get; set; }

        public List<WasteSubTypeModel> SubWasteTypes { get; set; }
    }
}
