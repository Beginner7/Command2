using System.Data.Entity;

namespace ChestClient.Models
{
    public class UserContext : DbContext
    {
        public DbSet<UserModel> UserModels { get; set; }
    }
}