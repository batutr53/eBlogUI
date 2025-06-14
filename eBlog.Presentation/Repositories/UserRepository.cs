﻿using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eBlog.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
            => await _dbSet.FirstOrDefaultAsync(x => x.Email == email);

        public async Task<User> GetByIdWithRolesAsync(Guid userId)
        {
            return await _context.Users
              .Include(u => u.UserRoles)
              .ThenInclude(ur => ur.Role) 
              .FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<User> GetByResetTokenAsync(string resetToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == resetToken);
        }

        public async Task<User> GetByEmailVerificationTokenAsync(string token)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.EmailVerificationToken == token);
        }
        public async Task<User> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users
                .Include(u => u.RefreshTokens)
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.RefreshTokens.Any(rt => rt.Token == refreshToken));
        }

        public async Task<User> GetByIdWithRefreshTokensAsync(Guid userId)
        {
            return await _context.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

    }
}
