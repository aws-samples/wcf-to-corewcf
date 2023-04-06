using WCF.SampleService.Contracts;

namespace WCF.SampleService.Services
{
    public class AuthService : IAuthService
    {
        public bool Authenticate(LoginInfo loginInfo)
        {
            // apply your logic here
            return true;
        }
    }
}
