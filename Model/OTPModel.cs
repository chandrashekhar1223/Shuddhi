namespace Shuddhi.Model
{
    public class OTPModel
    {
        public int OTPId { get; set; }
        public string MobileNumber { get; set; }
        public string OTP { get; set; }
        public DateTime OTPExpiry { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? VerifiedDate { get; set; }
    }
}
