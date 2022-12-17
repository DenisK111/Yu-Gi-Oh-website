using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Yu_Gi_Oh_website.Models;

namespace Yu_Gi_Oh_website.Tests.Moqs
{

    public class MoqUserManager : UserManager<ApplicationUser>
    {
        public MoqUserManager() : base(
                new Mock<IUserStore<ApplicationUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<ApplicationUser>>().Object,
                new IUserValidator<ApplicationUser>[0],
                new IPasswordValidator<ApplicationUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<ApplicationUser>>>().Object)
        { }

        public override Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return Task.FromResult(new ApplicationUser { Id = TestConstants.Forum.UserId });
        }

    }
}

