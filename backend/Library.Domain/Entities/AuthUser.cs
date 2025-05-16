using System;

namespace Library.Domain.Entities;

public class AuthUser
{
    public Guid Id { get; set; }

    public required string Mail { get; set; }

    public required string PassHash { get; set; }
}
