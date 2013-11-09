using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ChestClient.Models
{
    public class UserContext : DbContext
    {
        public DbSet<UserModel> UserModels { get; set; }
    }
}