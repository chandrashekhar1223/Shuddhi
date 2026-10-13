using Microsoft.AspNetCore.Mvc;
using Shuddhi.Repository;

namespace Shuddhi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class FAQController : Controller
    {
        private readonly IFAQRepository _faqRepository;

        public FAQController(IFAQRepository faqRepository)
        {
            _faqRepository = faqRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetFAQs()
        {
            var result = await _faqRepository.GetFAQs();

            return Ok(new
            {
                Success = true,
                Message = "FAQs fetched successfully.",
                Data = result
            });
        }
    }
}
