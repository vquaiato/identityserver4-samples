using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Services.InMemory;

namespace Server
{
    public class Config
    {
        public static IEnumerable<Scope> GetScopes() =>
            new List<Scope>
            {
                StandardScopes.OpenId,
                StandardScopes.Profile
            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    RedirectUris = { "http://localhost:5000/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5000" },

                    AllowedScopes =
                    {
                        StandardScopes.OpenId.Name,
                        StandardScopes.Profile.Name
                    }
                }
            };

        public static List<InMemoryUser> GetUsers()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "1",
                    Username = "alice",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Alice"),
                        new Claim("website", "https://alice.com")
                    }
                },
                new InMemoryUser
                {
                    Subject = "2",
                    Username = "bob",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Bob"),
                        new Claim("website", "https://bob.com")
                    }
                }
            };
        }
    }
}