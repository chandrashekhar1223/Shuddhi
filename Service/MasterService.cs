using Shuddhi.Database;
using Shuddhi.Model;
using Shuddhi.Repository;

namespace Shuddhi.Service
{
    public class MasterService: IMaster
    {
        
        public async Task<List<StateCity_Master>> GetStateCityMaster()
        {
         
            IConfiguration configuration = new ConfigurationBuilder()
   .SetBasePath(Directory.GetCurrentDirectory())
   .AddJsonFile("appsettings.json")
   .Build();
            try
            {


                MasterDB masterdb = new MasterDB(configuration);

                List<StateCity_Master> masterList = masterdb.GetStateCityMaster();

                return await Task.FromResult(masterList);
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                throw; // Rethrow the exception to be handled by the caller
            }
        }
    }
}
