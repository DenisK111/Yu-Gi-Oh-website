using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Yu_Gi_Oh_website.Models;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Models;
using Yu_Gi_Oh_website.Web.Data;

namespace Yu_Gi_Oh_website.Services.Forum.Implementations
{
    public class AspUserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public AspUserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IMapper mapper,ApplicationDbContext context)
        {

            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<bool> AddUserToRoleAsync(string userID, string role)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == userID);

            if (user == null) return false;

            var roleExists = await roleManager.Roles.AnyAsync(x => x.Name == role);

            if (!roleExists) return false;

            if ((await userManager.GetRolesAsync(user)).Contains(role)) return true;

            await userManager.AddToRoleAsync(user, role);

            return true;
        }

        public async Task<IEnumerable<UserInfoDto>> GetAllUsersWithRolesAsync()
        {
            var users = await userManager.Users.ToListAsync();
            var userDtos = mapper.Map<IEnumerable<UserInfoDto>>(users);

            foreach (var user in users)
            {
                userDtos.FirstOrDefault(x=>x.Id == user.Id)!.Roles = await userManager.GetRolesAsync(user);
            }
            return userDtos;
        }

        public async Task<UserInfoDto?> GetUserWithRolesAsync(string userID)
        {
            var user = await userManager.Users.Where(x=>x.Id == userID).SingleOrDefaultAsync();

            if (user is null) return null;

            var roles = await userManager.GetRolesAsync(user);

            var userDto = mapper.Map<UserInfoDto>(user);

            userDto.Roles = roles;

            return userDto;
        }

        public async Task<bool> RemoveUserFromRoleAsync(string userID, string role)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == userID);

            if (user == null) return false;

            var roleExists = await roleManager.Roles.AnyAsync(x => x.Name == role);

            if (!roleExists) return false;

            if ((await userManager.GetRolesAsync(user)).Contains(role)) await userManager.RemoveFromRoleAsync(user, role);

            return true;                      
        }

        public async Task<bool> UpdateProfilePictureAsync(string userId, string imageUrl)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) return false;

            user.ProfilePic = imageUrl;

            await context.SaveChangesAsync();

            return true;
        }
    }
}
