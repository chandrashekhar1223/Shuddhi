using Shuddhi.Database;
using Shuddhi.Model;

namespace Shuddhi.Repository
{
    public interface IUser
    {
        public Task<UserRegistration> RegisterUser(UserRegistration user);

        public Task<UserRegistration> GetUser(int UserId);


        public string EditUser(UserRegistration UserDetails);
    }
}
