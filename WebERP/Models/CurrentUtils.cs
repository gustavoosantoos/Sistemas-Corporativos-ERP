using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WebERP.Data.Repositories;

namespace WebERP.Models
{
    public class CurrentUtils
    {
        public readonly UserManager<ApplicationUser> UserManager;
        public readonly UserRepository UserRepository;
        public readonly IHttpContextAccessor Accessor;

        public CurrentUtils(UserManager<ApplicationUser> userManager, UserRepository userRepository, IHttpContextAccessor accessor)
        {
            UserManager = userManager;
            UserRepository = userRepository;
            Accessor = accessor;
        }
    }
}
