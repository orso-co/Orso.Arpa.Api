using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.UserDomain.Repositories
{
    public class ArpaUserManager : UserManager<User>
    {

        public ArpaUserManager(
            IUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<User>> logger) : base(
                store,
                optionsAccessor,
                passwordHasher,
                userValidators,
                passwordValidators,
                keyNormalizer,
                errors,
                services,
                logger)
        {
        }


        public async Task<User> FindUserByUsernameOrEmailAsync(string usernameOrEmail)
        {
            if (string.IsNullOrWhiteSpace(usernameOrEmail))
            {
                return null;
            }
            return usernameOrEmail.Contains('@') ? await FindByEmailAsync(usernameOrEmail) : await FindByNameAsync(usernameOrEmail);
        }

        public virtual Task<User> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Users.SingleOrDefaultAsync(u => u.Id.Equals(id), cancellationToken);
        }

        public async Task<IdentityResult> DeleteAsync(string userName)
        {
            User user = await FindByNameAsync(userName);

            return user == null ? throw new NotFoundException(nameof(User), "UserName") : await DeleteAsync(user);
        }

        public override async Task<IdentityResult> DeleteAsync(User user)
        {
            if (await CheckIfLastAdminWillBeRemovedAsync(user.Id))
            {
                throw new ValidationException(new ValidationFailure[]
                {
                    new ValidationFailure(nameof(user.UserName), "The operation is not allowed because it would remove the last administrator")
                });
            }

            IdentityResult result = await base.DeleteAsync(user);

            return !result.Succeeded ? throw new IdentityException("Problem deleting user", result.Errors) : result;
        }

        private async Task<bool> CheckIfLastAdminWillBeRemovedAsync(Guid userId)
        {
            IList<User> adminUsers = await GetUsersInRoleAsync(RoleNames.Admin);
            return adminUsers.Count == 1 && adminUsers[0].Id.Equals(userId);
        }
    }
}
