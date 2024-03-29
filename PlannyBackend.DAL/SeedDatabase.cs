﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using PlannyBackend.Model;
using PlannyBackend.Model.Enums;
using PlannyBackend.Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.DAL
{
    public static class SeedDatabase
    {
        private static IHostingEnvironment env { get; set; }

        public static void Seed(this ApplicationDbContext context)
        {
            var placeholderPicturesUrl = "https://localhost:44378/placeholderPictures";
            context.Database.EnsureCreated();
            if (!context.Users.Any())
            {
                context
                    .CreateUsers(placeholderPicturesUrl)
                    .CreateCategories()
                    .CreatePlannies(placeholderPicturesUrl);
            }
        }

        private static ApplicationDbContext CreateUsers(this ApplicationDbContext context, string webrootUrl)
        {
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

            var user = new ApplicationUser()
            {
                Email = "user@planny.com",
                NormalizedEmail = "USER@PLANNY.COM",
                UserName = "user@planny.com",
                NormalizedUserName = "USER@PLANNY.COM",
                EmailConfirmed = true,
                SecurityStamp = "345po5kl5m3k45m34k",
                PhoneNumber = "+311124211",
                PictureUrl = webrootUrl + "/user-placeholder.jpg",
                BirthDate = new DateTime(1995,04,13),
                SelfIntroduction = "Szeretem a kakaót."                 
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "Asdf123!");

            context.Users.Add(user);
            context.SaveChanges();

            return context;
        }

        private static ApplicationDbContext CreateCategories(this ApplicationDbContext context)
        {
            var mainCategory1 = new MainCategory()
            {
                Name = "sports",
            };
            context.MainCategories.Add(mainCategory1);

            var mainCategory2 = new MainCategory()
            {
                Name = "movies",
            };
            context.MainCategories.Add(mainCategory2);

            var mainCategory3 = new MainCategory()
            {
                Name = "music",
            };
            context.MainCategories.Add(mainCategory3);

            var mainCategory4 = new MainCategory()
            {
                Name = "travel",
            };
            context.MainCategories.Add(mainCategory4);

            var mainCategory5 = new MainCategory()
            {
                Name = "party",
            };
            context.MainCategories.Add(mainCategory5);

            var category6 = new MainCategory()
            {
                Name = "social",
            };
            context.MainCategories.Add(category6);

            var mainCategory7 = new MainCategory()
            {
                Name = "gaming",
            };
            context.MainCategories.Add(mainCategory7);

            var mainCategory8 = new MainCategory()
            {
                Name = "food",
            };
            context.MainCategories.Add(mainCategory8);

            for (int i = 1; i <= 8; i++)
            {
                var cat = new MainCategory()
                {
                    Name = "category" + i,
                };
                context.MainCategories.Add(cat);
            }
            context.SaveChanges();

            //sub
            var subCat1 = new Category()
            {
                Name = "Basketball",
                MainCategoryId = mainCategory1.Id
            };
            context.Categories.Add(subCat1);

            var subCat2 = new Category()
            {
                Name = "Swimming",
                MainCategoryId = mainCategory1.Id
            };
            context.Categories.Add(subCat2);

            var subCat3 = new Category()
            {
                Name = "Tennis",
                MainCategoryId = mainCategory1.Id
            };
            context.Categories.Add(subCat3);
            context.SaveChanges();

            return context;
        }

        private static ApplicationDbContext CreatePlannies(this ApplicationDbContext context, string webrootUrl)
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
                    FromTime = DateTime.Now.AddDays(2),
                    ToTime = DateTime.Now.AddDays(3),
                    MaxRequeredAge = 99,
                    MinRequeredAge = 18,
                    MaxParticipants = 10,
                    Participations = new List<Participation>(),
                    Location = location1,
                    PictureUrl = webrootUrl + "/planny-placeholder-" + ((i % 5) + 1) + ".jpg",
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
                    FromTime = DateTime.Now.AddDays(2),
                    ToTime = DateTime.Now.AddDays(3),
                    MaxRequeredAge = 99,
                    MinRequeredAge = 18,
                    MaxParticipants = 10,
                    Participations = new List<Participation>(),
                    Location = location2,
                    PictureUrl = webrootUrl + "/planny-placeholder-" + ((i % 5) + 1) + ".jpg",
                };
                context.Plannies.Add(planny);
            }

            context.SaveChanges();
            return context;
        }
    }
}
