using User_Microservices.UserModel;

namespace User_Microservices.IService
{
    public interface IUserService
    {
        public UserM AddUserDetail(UserM userM);
        public UserDisplay ViewUserDetail(int userID);

        public string Login(UserLogin login);
        public Task<string> ForgetPass(string Email);

        public Task<string> ResetPassword(string Password1, int userID);

    }
}
