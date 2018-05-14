using Microsoft.AspNetCore.Identity;
using PlannyBackend.Models;
using PlannyBackend.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Data
{
    public static class SeedDatabase
    {
        public static void Seed(this ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if (!context.Users.Any())
            {
                context
                    .CreateUsers()
                    .CreateCategories()
                    .CreatePlannyProposals();
            }
        }

        private static ApplicationDbContext CreateUsers(this ApplicationDbContext context)
        {
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

            var user = new ApplicationUser()
            {
                Email = "user@planny.com",
                NormalizedEmail = "USER@PLANNY.COM",
                UserName = "user@planny.com",
                NormalizedUserName = "USER@PLANNY.COM",
                EmailConfirmed = true,
                SecurityStamp = "345435kl5m3k45m34k",
                PhoneNumber = "+311124211",

            };
            user.PasswordHash = passwordHasher.HashPassword(user, "Asdf123!");

            context.Users.Add(user);
            context.SaveChanges();

            return context;
        }

        private static ApplicationDbContext CreateCategories(this ApplicationDbContext context)
        {
            var category1 = new Category()
            {               
                Name = "Kosárlabda",
            };
            context.Categories.Add(category1);


            var category2 = new Category()
            {
                Name = "Úszás",
            };
            context.Categories.Add(category2);

            var category3 = new Category()
            {
                Name = "Paintball",
            };
            context.Categories.Add(category3);

            var category4 = new Category()
            {
                Name = "Film",
            };
            context.Categories.Add(category4);

            var category5 = new Category()
            {
                Name = "Kirándulás",
            };
            context.Categories.Add(category5);


            context.SaveChanges();

            return context;
        }

        private static ApplicationDbContext CreatePlannyProposals(this ApplicationDbContext context)
        {
            for (int i = 0; i < 5; i++)
            {
                var planny = new PlannyProposal()
                {
                    OwnerId = 1,
                    Name = "Planny " + i,
                    Description = "Discription for planny number " + i,
                    CategoryId = 1,
                    FromTime = new DateTime().AddDays(7),
                    ToTime = new DateTime().AddDays(8),
                    IsNearOwner = false,
                    IsOnlyForFriends = false,
                    IsSimilarInterest = false,
                    MaxAge = 99,
                    MinAge = 18,
                    MaxParticipants = 10,
                    MinParticipants = 1,
                    Participations = new List<Participation>() 
                };
                context.PlannyProposals.Add(planny);             
            }

            context.SaveChanges();
            return context;
        }
    } 
}
