// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using OnlineShop.Library.Constants;
using System.Collections.Generic;

namespace OnlineShop.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                // будет использоваться для того, чтобы услуги взаимодействовали между собой
                new ApiScope(IdConstants.ApiScope),
                // будет использоваться пользователями через веб интерфейс
                new ApiScope(IdConstants.WebScope),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // клиент для взаимодейтсвия услуг м-ду собой
                new Client
                {
                    ClientId = "test.client",
                    ClientName = "Test client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdConstants.ApiScope,
                        IdConstants.WebScope
                    }
                },

                // клиент который будет заходить с.п. логина и пароля
                new Client
                {
                    ClientId = "external",
                    ClientName = "External Client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireClientSecret = false,

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "OnlineShop.Web"
                    }
                },
            };
    }
}