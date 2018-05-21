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
                Name = "sports",
            };
            context.Categories.Add(category1);

            var category2 = new Category()
            {
                Name = "movies",
            };
            context.Categories.Add(category2);

            var category3 = new Category()
            {
                Name = "music",
            };
            context.Categories.Add(category3);

            var category4 = new Category()
            {
                Name = "travel",
            };
            context.Categories.Add(category4);

            var category5 = new Category()
            {
                Name = "party",
            };
            context.Categories.Add(category5);

            var category6 = new Category()
            {
                Name = "social",
            };
            context.Categories.Add(category6);

            var category7 = new Category()
            {
                Name = "gaming",
            };
            context.Categories.Add(category7);

            var category8 = new Category()
            {
                Name = "food",
            };
            context.Categories.Add(category8);
            context.SaveChanges();

            //sub
            var subCat1 = new Category()
            {
                Name = "Basketball",
                ParentCategoryId = category1.Id
            };
            context.Categories.Add(subCat1);

            var subCat2 = new Category()
            {
                Name = "Swimming",
                ParentCategoryId = category1.Id
            };
            context.Categories.Add(subCat2);

            var subCat3 = new Category()
            {
                Name = "Tennis",
                ParentCategoryId = category1.Id
            };
            context.Categories.Add(subCat3);
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
