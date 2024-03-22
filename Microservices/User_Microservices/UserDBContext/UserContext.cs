using Microsoft.EntityFrameworkCore;
using User_Microservices.UserEntity;

namespace User_Microservices.UserDBContext
{
    public class UserContext :DbContext
    {
        public UserContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<UserEntity1>User { get; set; }        
    }
}
