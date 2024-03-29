﻿using Microsoft.AspNetCore.Identity;

namespace SiimpleWebApp.Models;

public class AppUser :IdentityUser
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
}
