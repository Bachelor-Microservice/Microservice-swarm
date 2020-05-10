using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer
{
      public  class Config
    {
        public string Environment { get; set; }
        

        public Config()
        {
            
      
        }

        public void setEnvironemnt(string env)
        {
            Environment = env;
            if (string.IsNullOrEmpty(Environment))
            {
               
                    Environment = "http://localhost:4200";
                
            }
        }
        public  List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "test",
                    Password = "test",

                    Claims = new []
                    {
                        new Claim("role" , "client"), 
                    }
                },
                
            };
        }

        public  IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                
                new IdentityResource("roles" , "Your'e roles" ,  new List<string>{"role"})
            };
        }

        public  IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api", "My API")
            };
        }

        public  IEnumerable<Client> GetClients()
        {
            System.Console.WriteLine("Client" + "  " + Environment);
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api" }
                },
                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api" }
                },
                // OpenID Connect hybrid flow client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris           = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api"
                    },

                    AllowOfflineAccess = true
                },
                // JavaScript Client
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA Code Client",
                    AccessTokenLifetime = 33000,// 330 seconds, default 60 minutes
                    IdentityTokenLifetime = 300,
                    RequireConsent = false,
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    AlwaysIncludeUserClaimsInIdToken =  true,
                    AllowOfflineAccess = true,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                       
                         //Environment ,
                          "http://34.77.231.255/" ,
                        // "http://localhost" 
                       // "http://localhost" + "/silent-refresh.html"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        
                        //Environment + "" ,
                        "http://34.77.231.255/",
                       // "http://localhost:4200" 
                        
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        
                       // Environment,
                        "hhttp://34.77.231.255/",
                      //  "http://localhost:4200" 
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        IdentityServerConstants.StandardScopes.Email,
                        "api"
                    }
                    
                
                }
            };
        }
    }
}