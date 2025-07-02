using System;

namespace Library.Domain.Entities;

public class AuthUser : BaseEntity
{
    public required string Mail { get; set; }

    public required string PassHash { get; set; }
}
