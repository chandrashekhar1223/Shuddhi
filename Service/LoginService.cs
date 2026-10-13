using Shuddhi.Database;
using Shuddhi.Model;
using Shuddhi.Repository;

namespace Shuddhi.Service
{
    public class LoginService : ILogin
    {

       
        public Task<Login> Login(Login login)
        {

            IConfiguration configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json")
.Build();
            try
            {
                LoginDB login1 = new LoginDB(configuration);

                Login login2 = new Login();

                Login result = login1.Login(login.UserId, login.Password);



                // configuration.Bind("Login", login);
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                throw; // Rethrow the exception to be handled by the caller
            }
        }
    }
}
