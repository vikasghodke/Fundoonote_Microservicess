namespace User_Microservices.UserModel
{
    public class Reset_PasswordModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Confirm_Password { get; set; }
    }
}
