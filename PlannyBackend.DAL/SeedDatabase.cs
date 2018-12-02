using Microsoft.AspNetCore.Identity;
using PlannyBackend.Model;
using PlannyBackend.Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.DAL
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

            for(int i = 1; i <= 8; i++)
            {
                var cat = new Category()
                {
                    Name = "category" + i,
                };
                context.Categories.Add(cat);
            }
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

            var categories = context.Categories.ToList();
            var r = new Random();

            var location1 = new Location()
            {
                Address = "Budapest",
                Latitude = 47.49801,
                Longitude = 19.03991,
            };

            var location2 = new Location()
            {
                Address = "Győr",
                Latitude = 47.68333,
                Longitude = 17.63512
            };

            for (int i = 0; i < 5; i++)
            {
                var planny = new Planny()
                {
                    OwnerId = 1,
                    Name = "Planny " + i,
                    Description = "Discription for planny number " + i,
                    PlannyCategories = categories.GetRange(0, r.Next(0, categories.Count - 1))
                                  .Select(c => new PlannyCategory() { Category = c }).ToList(),
                    FromTime = DateTime.Now.AddDays(7),
                    ToTime = DateTime.Now.AddDays(8),
                    IsNearOwner = false,
                    IsOnlyForFriends = false,
                    IsSimilarInterest = false,
                    MaxAge = 99,
                    MinAge = 18,
                    MaxParticipants = 10,
                    MinParticipants = 1,
                    Participations = new List<Participation>(),
                    Location = location1,
                    PictureUrl = "14548827-e257-4804-9e2a-c681514444af"
                };
                context.Plannies.Add(planny);
            }

            for (int i = 5; i < 7; i++)
            {
                var planny = new Planny()
                {
                    OwnerId = 1,
                    Name = "Planny " + i,
                    Description = "Discription for planny number " + i,
                    PlannyCategories = categories.GetRange(0, r.Next(0, categories.Count - 1))
                                  .Select(c => new PlannyCategory() { Category = c }).ToList(),
                    FromTime = DateTime.Now.AddDays(7),
                    ToTime = DateTime.Now.AddDays(8),
                    IsNearOwner = false,
                    IsOnlyForFriends = false,
                    IsSimilarInterest = false,
                    MaxAge = 99,
                    MinAge = 18,
                    MaxParticipants = 10,
                    MinParticipants = 1,
                    Participations = new List<Participation>(),
                    Location = location2,
                    PictureUrl = "14548827-e257-4804-9e2a-c681514444af"
                };
                context.Plannies.Add(planny);
            }

            context.SaveChanges();
            return context;
        }
    }
}
