using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityService
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
             new IdentityResource("roles", new[] { "role" })

            };


        public static IEnumerable<ApiScope> ApiScopes =>
           new List<ApiScope>
           {
             new ApiScope("misyatagimApi", "Misyatagim API")
           };
        public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
              new ApiResource("misyatagimApi", "Misyatagim API")
                {
                    Scopes = { "misyatagimApi" }
                }
         };

        public static IEnumerable<Client> Clients =>
          new List<Client>
            {
                  new Client
                  {
                    ClientId = "misyatagim_client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = {  IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Profile,
                          "roles",
                           "misyatagimApi"
                      },

                    AllowOfflineAccess = true,
                    
                  },
            };

        public static List<TestUser> TestUsers =>
        new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "admin",
                    Password = "password",
                    Claims = new List<Claim>()
                    {
                         new Claim(ClaimTypes.Name, "admin"),
                         new Claim(ClaimTypes.Role, "admin"),
                    }
                }
            };
    }
}