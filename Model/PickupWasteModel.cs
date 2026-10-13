namespace Shuddhi.Model
{
    public class PickupWasteModel
    {
        public int? WasteTypeId { get; set; }

          public List<int> WasteSubTypeId { get; set; }


        public string? WasteSubTypeId1 { get; set; }

        public decimal? Quantity { get; set; }

      //  public IFormFile? Photo { get; set; }
      //  
      public string? PhotoPath { get; set; }


       // public int Was
       public int? ID { get; set; }


        public String? Status { get; set; }
    }
}
