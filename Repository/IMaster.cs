using Shuddhi.Model;

namespace Shuddhi.Repository
{
    public interface IMaster
    {

        public Task<List<StateCity_Master>> GetStateCityMaster();
    }
}
