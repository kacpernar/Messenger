// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using IdentityModel;
using System.Security.Claims;
using System.Text.Json;
using Duende.IdentityServer;
using Duende.IdentityServer.Test;

namespace IdentityServer;

public class TestUsers
{
    public static List<TestUser> Users
    {
        get
        {
            var address = new
            {
                street_address = "One Hacker Way",
                locality = "Heidelberg",
                postal_code = 69118,
                country = "Germany"
            };
                
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "Natalia",
                    Password = "Natalia",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Natalia"),
                        new Claim(JwtClaimTypes.GivenName, "Natalia"),
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "Jedi",
                    Password = "Jedi",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Jedi"),
                        new Claim(JwtClaimTypes.GivenName, "Jedi"),
                    }
                },
                new TestUser
                {
                    SubjectId = "3",
                    Username = "Miki",
                    Password = "Miki",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Miki"),
                        new Claim(JwtClaimTypes.GivenName, "Miki"),
                    }
                },
                new TestUser
                {
                    SubjectId = "4",
                    Username = "Kacper",
                    Password = "Kacper",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Kacper"),
                        new Claim(JwtClaimTypes.GivenName, "Kacper"),
                    }
                }
            };
        }
    }
}