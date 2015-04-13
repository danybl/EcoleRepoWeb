using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EcoleWeb.Models
{
    public class AppDbContext : IdentityDbContext<etudiant>
    {
        public AppDbContext() : base("LocalMySqlServer") { }
        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}