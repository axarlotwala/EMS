using EMS.Model;

namespace EMS.DataAccess.Interface
{
    public interface ILoginRepository
    {
        //Task<LoginResponse> AdminLogin(RequestLogin request);
        Task<LoginUser> UserLogin(RequestLogin request);

    }
}
