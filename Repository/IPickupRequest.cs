using Shuddhi.Model;

namespace Shuddhi.Repository
{
    public interface IPickupRequest
    {

        public Task<DryWaste> AddDryWaste(DryWaste dryWaste);


        public Task<EWaste> AddEWaste(EWaste eWaste);


        public Task<WetWaste> AddWetWaste(WetWaste wetWaste);


        public Task<OtherWaste> AddOtherWaste(OtherWaste otherWaste);

        public Task<PickupRequestModel> AddPickupRequest(PickupRequestModel pickupRequestModel);



        public Task<List<PickupRequestModel>> GetPickuprequestdetails1(int pickupRequestId);


        public Task<List<PickupRequestModel>> GetPendingPickupRequests();


        public bool UpdatePickupRequestStatus(int ID, string Status);

    }
}
