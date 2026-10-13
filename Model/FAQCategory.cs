namespace Shuddhi.Model
{
    public class FAQCategory
    {
        public int CategoryId { get; set; }
        public string CategoryCode { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public string Tip { get; set; }
        public List<FAQQuestion> Questions { get; set; } = new List<FAQQuestion>();
    }
}
