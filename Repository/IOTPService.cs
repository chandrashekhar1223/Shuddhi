namespace Shuddhi.Repository
{
    public interface IOTPService
    {
        Task<int> SendOTP(string mobileNumber);
        Task<bool> VerifyOTP(string mobileNumber, string otp);

        Task<bool> CheckMobileNumber(string mobileNumber,int UserType);

        Task<bool> ChangePassword(
      string mobileNumber,
      string newPassword,
            int userType);
    }
}
