// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityProvider
{
    public static class Config
    {
      


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                // the api requires the role claim
                new ApiResource("example1", "The Weather API", new[] { JwtClaimTypes.Role }),
                  new ApiResource("ProtectedGrpc")
                {
                    DisplayName = "API protected",
                    ApiSecrets =
                    {
                        new Secret("grpc_protected_secret".Sha256())
                    },
                    Scopes = new List<string>{"grpc_protected_scope" },
                    UserClaims = { "role", "admin", "user", "safe_zone_api" }
                }
            };

        public static IEnumerable<ApiScope> Scopes =>
        new ApiScope[]
        {
           
                new ApiScope("grpc_protected_scope", "grpc_protected_scope")
            };
        

        public static IEnumerable<Client> Clients =>
           

            new Client[]
            {
                

                new Client
                {
                    ClientId = "ProtectedGrpc",
                    
                // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,  //just secret

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("grpc_protected_secret".Sha256())
                    },
                  
                    AllowedScopes = {"example1","grpc_protected_scope" },
                 
                    Enabled = true
                }
                ,
            };
        
    }
}