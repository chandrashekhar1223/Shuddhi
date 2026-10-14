using Shuddhi.Model;

namespace Shuddhi.Repository
{
    public interface ISocietyService
    {
        //Task<int> AddSocietyDB(AddSociety model);

        public Task<int> AddSociety(AddSociety model);

         Task<List<AddSociety>> GetSocieties(string search);

        Task<AddSociety> GetSocietyById(int id);

        Task<(bool , string )> UpdateSociety(AddSociety model);

        Task<object> DeleteSociety(int id);


        Task<List<AddSociety>> GetSocietieslist();

        Task<List<AddSociety>> GetSocietyListByPincode(GetSocieties Pincode);
    }
}
