using Shuddhi.Database;
using Shuddhi.Repository;

namespace Shuddhi.Service
{
    public class OTPService : IOTPService
    {
        IConfiguration configuration = new ConfigurationBuilder()
  .SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json")
  .Build();

        public Task<bool> ChangePassword(string mobileNumber, string newPassword,int usertype)
        {
            OTPManageDB otp = new OTPManageDB(configuration);
            return otp.ChangePassword(mobileNumber, newPassword,usertype);
            
        }

        public Task<bool> CheckMobileNumber(string mobileNumber , int UserType)
        {
            OTPManageDB otp = new OTPManageDB(configuration);
           return otp.CheckMobileNumber(mobileNumber,UserType);

        }

        public Task<int> SendOTP(string mobileNumber)
        {
            OTPManageDB otp = new OTPManageDB(configuration);

            return  otp.SendOTP(mobileNumber);

        }

        public Task<bool> VerifyOTP(string mobileNumber, string otp)
        {
           OTPManageDB OTP=new OTPManageDB(configuration);
            
            return OTP.VerifyOTP(mobileNumber, otp);
        }
    }
}
