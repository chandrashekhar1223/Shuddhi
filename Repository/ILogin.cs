using Shuddhi.Model;

namespace Shuddhi.Repository
{
    public interface ILogin
    {
        public Task<Login> Login(Login login);
    }
}
