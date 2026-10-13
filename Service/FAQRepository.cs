using Shuddhi.Model;
using System.Data;
using System.Data.SqlClient;
using Shuddhi.Repository;

namespace Shuddhi.Service
{
    public class FAQRepository : IFAQRepository
    {
        private readonly IConfiguration _configuration;

        public FAQRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<FAQCategory>> GetFAQs()
        {
            List<FAQCategory> categories = new List<FAQCategory>();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetFAQs", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    await con.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            categories.Add(new FAQCategory
                            {
                                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                CategoryCode = reader["CategoryCode"].ToString(),
                                Title = reader["Title"].ToString(),
                                SubTitle = reader["SubTitle"].ToString(),
                                Icon = reader["Icon"].ToString(),
                                Color = reader["Color"].ToString(),
                                Tip = reader["Tip"].ToString()
                            });
                        }

                        await reader.NextResultAsync();

                        while (await reader.ReadAsync())
                        {
                            int categoryId = Convert.ToInt32(reader["CategoryId"]);

                            var category = categories.FirstOrDefault(x => x.CategoryId == categoryId);

                            if (category != null)
                            {
                                category.Questions.Add(new FAQQuestion
                                {
                                    FAQId = Convert.ToInt32(reader["FAQId"]),
                                    Question = reader["Question"].ToString(),
                                    Answer = reader["Answer"].ToString()
                                });
                            }
                        }
                    }
                }
            }

            return categories;
        }
    }
}
