using Shuddhi.Model;

namespace Shuddhi.Repository
{
    public interface IVendor
    {
        public Task<VendorRegistration> RegisterVendor(VendorRegistration vendor);


        Task<VendorRegistration> GetVendorById(int vendorId);

        Task<Login> VendorLogin(Login login);


        Task<string> EditVendor(VendorRegistration Vendor);
    }
}
