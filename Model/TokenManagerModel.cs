using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Shuddhi.Model
{
    public class TokenManagerModel
    {

        public int PickupRequestId { get; set; }
        public string Name { get; set; }

        public string PhotoPath { get; set; }

        public double QUANTITY { get; set; }

        public string Status { get; set; }

        public string Address { get; set; }


        public string Society { get; set; }
       // public List<string> Assign { get; set; }

        public int WasteTypeId { get; set; }

        public string Sub_Waste_Type { get; set; }

        public string Note { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public Decimal TotalEstimatedWaste { get; set; }


     

        public string CreatedDate { get; set; }


        public List<VendorRegistration> Assign { get; set; }


        public string? pincode { get; set; }

       // public int PickupRequestId { get; set; }
       public string Assigned_Vendor_Id { get; set; }


     //  public List<int> Subwastype_Id { get; set; }




    }
}
