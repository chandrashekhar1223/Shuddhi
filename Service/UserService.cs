using Shuddhi.Database;
using Shuddhi.Model;
using Shuddhi.Repository;
using System.Numerics;

namespace Shuddhi.Service
{
    public class UserService : IUser
    {
        IConfiguration configuration = new ConfigurationBuilder()
   .SetBasePath(Directory.GetCurrentDirectory())
   .AddJsonFile("appsettings.json")
   .Build();
        public  Task<UserRegistration> GetUser(int UserId)
        {
            try
            {
                UserDB userDB = new UserDB(configuration);
                UserRegistration Userdetails = userDB.GetUser(UserId);
                // throw new NotImplementedException();
                return Task.FromResult(Userdetails);
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                throw; // Rethrow the exception to be handled by the caller
            }
        }

        public Task<UserRegistration> RegisterUser(UserRegistration user)
        {
            string message= string.Empty;
            // UserDB userdb = new UserDB("Data Source=DESKTOP-IOODVEQ;Initial Catalog=Shuddhi;User ID=sa;Password=123;TrustServerCertificate=True;");
            try
            {
                UserDB userdb = new UserDB(configuration);
                int id = userdb.InsertUserRegistration(user);
                user.ID = id;

                var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "User_Documents");
                var vendorFolder = Path.Combine(baseFolder, id.ToString());


                if (!Directory.Exists(vendorFolder))
                {
                    Directory.CreateDirectory(vendorFolder);
                }

               

                var businessLicenceFolder = Path.Combine(vendorFolder, "Profile_Photo");

                if (!Directory.Exists(businessLicenceFolder))
                {
                    Directory.CreateDirectory(businessLicenceFolder);
                }

                if (user.User_Profile_Photo != null)
                {
                    var fileName = user.User_Profile_Photo.FileName;

                    var filePath = Path.Combine(businessLicenceFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        user.User_Profile_Photo.CopyTo(stream);
                    }


                    user.User_Profile_photo_url =
                    Path.Combine("User_Documents", id.ToString(),
                    "Profile_Photo", fileName);



                    userdb.Update_Profilephoto_UserRegistration(id, user.User_Profile_photo_url);

                }
              



                return Task.FromResult(user);


            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                throw; // Rethrow the exception to be handled by the caller
            }
        }


        public string EditUser(UserRegistration UserDetails)
        {
            var message = string.Empty;
            try
            {
                UserDB userDB = new UserDB(configuration);
               message= userDB.UpdateUserRegistration(UserDetails);

                


                
            }
            catch(Exception ex)
            {
                FileLogger.LogError(ex);
            }

            return message;

        }

    }
    
}
