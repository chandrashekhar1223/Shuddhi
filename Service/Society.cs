using Shuddhi.Database;
using Shuddhi.Model;
using Shuddhi.Repository;

namespace Shuddhi.Service
{
    public class Society : ISocietyService
    {
        IConfiguration configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json")
.Build();

       
        public async  Task<int> AddSociety(AddSociety model)
        {
            SocietyDB society = new SocietyDB(configuration);

           int id=await society.AddSociety(model);

           return id;
        }

        public async Task<object> DeleteSociety(int id)
        {
            SocietyDB society = new SocietyDB(configuration);
          var result =await society.DeleteSociety(id);

            return result;

            
        }

        public async Task<List<AddSociety>> GetSocieties(string search)
        {
            SocietyDB society = new SocietyDB(configuration);

            List<AddSociety> societieslist = new List<AddSociety>();

            societieslist =await society.GetSocieties(search);


            return societieslist;
        }

        public async Task<List<AddSociety>> GetSocietieslist()
        {

            SocietyDB society = new SocietyDB(configuration);

            List<AddSociety> societieslist = new List<AddSociety>();

            societieslist = await society.GetSocietyList();

            return societieslist;
           
        }

        public Task<AddSociety> GetSocietyById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AddSociety>> GetSocietyListByPincode(GetSocieties Pincode)
        {
            SocietyDB society = new SocietyDB(configuration);

            List<AddSociety> societieslist = new List<AddSociety>();

            societieslist = await society.GetSocietyListByPincode(Pincode);

            return societieslist;
        }

        public async Task<(bool, string)> UpdateSociety(AddSociety model)
        {
            SocietyDB society = new SocietyDB(configuration);

            var result= await society.UpdateSociety(model);

            return result;

        }




    }
}
