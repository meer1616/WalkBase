﻿using Microsoft.AspNetCore.Identity;

namespace Authentication.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);

    }
}
