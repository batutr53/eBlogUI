using eBlog.Domain.Entities;
using eBlog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eBlog.Persistence.Seeders;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.Database.MigrateAsync();

        if (!context.Roles.Any())
        {
            var roles = new List<Role>
            {
                new Role { Id = Guid.NewGuid(), Name = "Admin" },
                new Role { Id = Guid.NewGuid(), Name = "Editor" },
                new Role { Id = Guid.NewGuid(), Name = "User" }
            };
            context.Roles.AddRange(roles);
            await context.SaveChangesAsync();
        }

        // Admin Kullanıcısı
        if (!context.Users.Any(u => u.Email == "admin@blog.com"))
        {
            var admin = new User
            {
                Id = Guid.NewGuid(),
                Email = "admin@blog.com",
                UserName = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                IsEmailVerified = true,
                CreatedAt = DateTime.UtcNow,
                PasswordResetToken = Guid.NewGuid().ToString(),
                ResetTokenExpires = DateTime.UtcNow.AddHours(1),
                EmailVerificationToken = Guid.NewGuid().ToString()
            };

            context.Users.Add(admin);
            await context.SaveChangesAsync();

            var adminRole = await context.Roles.FirstAsync(r => r.Name == "Admin");
            context.UserRoles.Add(new UserRole { UserId = admin.Id, RoleId = adminRole.Id });
            await context.SaveChangesAsync();
        }

        // Örnek Kategori
        if (!context.Categories.Any())
        {
            var cat = new Category { Id = Guid.NewGuid(), Name = "Genel", Slug = "genel" };
            context.Categories.Add(cat);
            await context.SaveChangesAsync();
        }

        // Örnek Ürün
        if (!context.Products.Any())
        {
            var cat = await context.Categories.FirstAsync();
            var admin = await context.Users.FirstAsync(u => u.Email == "admin@blog.com");

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Deneme Ürünü",
                Slug = "deneme-urunu",
                Description = "Bu bir örnek üründür.",
                Price = 99.99m,
                Stock = 10,
                CategoryId = cat.Id,
                SellerId = admin.Id, // 🧠 Hata buradaydı
                CreatedAt = DateTime.UtcNow
            };
            context.Products.Add(product);
            await context.SaveChangesAsync();
        }


        // Örnek Post
        if (!context.Posts.Any())
        {
            var admin = await context.Users.FirstAsync(u => u.Email == "admin@blog.com");
            var category = await context.Categories.FirstAsync();

            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = "Hoşgeldiniz!",
                Content = "Bu, blog sistemimizin ilk yazısıdır.",
                Slug = "hosgeldiniz",
                AuthorId = admin.Id,
                UserId = admin.Id,
                CategoryId = category.Id,
                IsPublished = true,
                PublishedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            context.Posts.Add(post);
            await context.SaveChangesAsync();
        }
    }
}
