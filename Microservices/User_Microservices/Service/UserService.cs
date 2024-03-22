using NuGet.Common;
using User_Microservices.Hashing;
using User_Microservices.IService;
using User_Microservices.Jwt_Token;
using User_Microservices.UserDBContext;
using User_Microservices.UserEntity;
using User_Microservices.UserModel;

namespace User_Microservices.Service
{
    public class UserService: IUserService
    {
        private readonly UserContext _userContext;
        private readonly IConfiguration _config;
        private readonly Hash_password _Password;

        public UserService(UserContext userContext, IConfiguration config,Hash_password password)
        {
            this._userContext = userContext;
            this._config = config;
            this._Password = password;

        }
        public UserM AddUserDetail(UserM userM)
        {
            UserEntity1 entity = new UserEntity1();
            entity.Name= userM.Name;
            entity.Email= userM.Email;
            entity.Password= _Password.HashPassword( userM.Password);

            _userContext.User.Add(entity);
            _userContext.SaveChanges();
            return userM;
        }
        public UserDisplay ViewUserDetail(int userID)
        {
            var result=_userContext.User.FirstOrDefault(e=>e.Id==userID);
            if (result != null)
            {
                UserDisplay display = new UserDisplay();
                display.Name= result.Name;
                display.Email= result.Email;

                return display;
            }
            return null;

        }

        public string Login(UserLogin login)
        {
            UserEntity1 valid=_userContext.User.FirstOrDefault(e=>e.Email==login.Email);
            Jwt_Token1 token = new Jwt_Token1(_config);
            if(valid!=null)
            {
                bool Pass=_Password.VerifyPassword(login.Password, valid.Password);
                if(Pass)
                {
                    return token.GenereateToken(valid);
                }
            }
            return null;
        }
        public async Task<string> ForgetPass(string Email)
        {
            var user = _userContext.User.FirstOrDefault(e=>e.Email==Email);
            Jwt_Token1 token = new Jwt_Token1(_config);
            if (user != null)
            {
                string _token = token.GenerateTokenReset(Email, user.Id);

                var url = $"https://localhost:7269/api/User/ForgetPass?token={_token}";

                EmailService service = new EmailService();
                await service.SendEmailAsync(Email, "Reset Password", url);
                return "Ok";
            }
            return null;
        }
        public async Task<string> ResetPassword(string Password1, int userID)
        {
            var user = _userContext.User.FirstOrDefault(e => e.Id == userID);
            Jwt_Token1 token = new Jwt_Token1(_config);
            if (user != null)
            {
                string result = _Password.HashPassword(Password1);
                user.Password = result;
                _userContext.SaveChanges();
            }
            return Password1;


        }

    }
}
