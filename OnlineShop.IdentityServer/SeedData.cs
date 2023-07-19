// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using OnlineShop.Library.UserManagmentService.Models;
using OnlineShop.Library.Data;
using OnlineShop.Library.Common.Models;

namespace OnlineShop.IdentityServer
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<UsersDbContext>(options =>
               options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<UsersDbContext>()
                .AddDefaultTokenProviders();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<UsersDbContext>();
                    context.Database.Migrate();

                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var firstuser = userMgr.FindByNameAsync("firstuser").Result;
                    if (firstuser == null)
                    {
                        firstuser = new ApplicationUser
                        {
                            UserName = "firstuser",
                            FirstName = "Firstuser",
                            LastName = "Foreverfirst",
                            Email = "firstuser@email.com",
                            EmailConfirmed = true,
                            DefaultAddress = new Address()
                            {
                                City = "Forgotten",
                                Country = "NoOneNeeds",
                                PostalCode = "12345",
                                AddressLine1 = "Who cares, 3",
                                AddressLine2 = "Nobody, 4"
                            },
                            DeliveryAddress = new Address()
                            {
                                City = "Dont ask",
                                Country = "Ill not say",
                                PostalCode = "54321",
                                AddressLine1 = "AddressLine1, 3",
                                AddressLine2 = "AddressLine2, 4"
                            }
                        };
                        var result = userMgr.CreateAsync(firstuser, "firstUser123%").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(firstuser, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Firstuser Foreverfirst"),
                            new Claim(JwtClaimTypes.GivenName, "Firstuser"),
                            new Claim(JwtClaimTypes.FamilyName, "Foreverfirst"),
                            new Claim(JwtClaimTypes.WebSite, "http://websitenotfound.com"),
                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug("Firstuser created");
                    }
                    else
                    {
                        Log.Debug("Firstuser already exists");

                        if(firstuser.DefaultAddress == null)
                        {
                            firstuser.DefaultAddress = new Address()
                            {
                                City = "Forgotten",
                                Country = "NoOneNeeds",
                                PostalCode = "12345",
                                AddressLine1 = "Who cares, 3",
                                AddressLine2 = "Nobody, 4"
                            };
                        }

                        if(firstuser.DeliveryAddress == null)
                        {
                            firstuser.DeliveryAddress = new Address()
                            {
                                City = "Dont ask",
                                Country = "Ill not say",
                                PostalCode = "54321",
                                AddressLine1 = "AddressLine1, 3",
                                AddressLine2 = "AddressLine2, 4"
                            };
                        }

                        var result = userMgr.UpdateAsync(firstuser).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug("Firstuser has been updated");
                    }
                }
            }
        }
    }
}
