using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace BankSystem.Infrastructure.SeedData;


public static class SeedData
{
    public static void SeedDataModel(this ModelBuilder builder)
    {
        Role adminRole = new() { Id = 1, Name = "Admin" };
        Role userRole = new() { Id = 2, Name = "User" };
        builder.Entity<Role>().HasData([adminRole, userRole]);

        var userid = Guid.NewGuid();
        User user = new()
        {
            ID = userid,
            First_Name = "Admin-FirstName",
            Last_Name = "Admin-LastName",
            Password = BC.HashPassword("1234"),
            Email = "Admin@Admin.com",
            UserName = "Admin-UserName"
        };


        builder.Entity<User>().HasData(user);

        builder.Entity<UserRole>().HasData
        ([
            new() { Id = 1, UserId = userid, RoleId = userRole.Id },
            new() { Id = 2, UserId = userid, RoleId = adminRole.Id }
        ]);
       
    }
}