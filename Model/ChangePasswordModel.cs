namespace Shuddhi.Model
{
    public class ChangePasswordModel
    {
        public string MobileNumber { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

        public int usertype { get; set; }  
    }
}
