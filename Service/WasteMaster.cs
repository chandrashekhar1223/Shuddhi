using Shuddhi.Database;
using Shuddhi.Model;

namespace Shuddhi.Service
{
    public class WasteMaster
    {
        IConfiguration configuration = new ConfigurationBuilder()
  .SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json")
  .Build();
        public WasteMasterResponse GetWasteMaster()
        {
            try
            {
                // WasteDAL objDAL = new WasteDAL();
                GetWasteMaster objDAL = new GetWasteMaster(configuration);

                return objDAL.GetWasteMaster1();
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                throw; // Rethrow the exception to be handled by the caller
            }
        }
    }
}
