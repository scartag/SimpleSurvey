using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Auth
{
    public class Config
    {
        public static string RootUrl { get; set; } = "http://localhost:49341";


        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {

                new ApiResource("api1", "Survey API")

            };
        }


        public static IEnumerable<Client> GetClients()
        {


            return new List<Client>

            {


                new Client
                {
                     ClientId = "client",

            // no interactive user, use the clientid/secret for authentication
            AllowedGrantTypes = GrantTypes.Implicit,

            // secret for authentication
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },

            //AlwaysIncludeUserClaimsInIdToken = true,

            // scopes that client has access to
            AllowedScopes = {IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email,
                    "api1"}
        },

                new Client
        {
            ClientId = "mvc",
            ClientName = "MVC Client",
            AllowedGrantTypes = GrantTypes.Implicit,

            // where to redirect to after login
            RedirectUris = { $"{RootUrl}/signin-oidc" },

            //AlwaysIncludeUserClaimsInIdToken = true,
            //AlwaysSendClientClaims = true, 

            // where to redirect to after logout
            PostLogoutRedirectUris = { $"{RootUrl}/signout-callback-oidc" },

            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email
            }
        },

                 // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    //AlwaysIncludeUserClaimsInIdToken = true,
                    //AlwaysSendClientClaims = true,
                    
                     

                    RedirectUris = { $"{RootUrl}/survey" },
                    PostLogoutRedirectUris = { $"{RootUrl}/home/loggedout" },
                    AllowedCorsOrigins = { $"{RootUrl}" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api1"
                    },
                }
        };


        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.Profile(),
                 new IdentityResources.OpenId(),
            new IdentityResources.Email
            {
                Required = true
            }
           };
        }

    }
}
