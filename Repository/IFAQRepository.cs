using Shuddhi.Model;

namespace Shuddhi.Repository
{
    public interface IFAQRepository
    {
        Task<List<FAQCategory>> GetFAQs();
    }
}
