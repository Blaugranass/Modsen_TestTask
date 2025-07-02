using System;

namespace Library.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public required string Token { get; set; } 

    public DateTime Expires { get; set; }

    public Guid UserId { get; set; }

    public AuthUser User { get; set; } = null!;

    public bool IsRevoked { get; set; }
}
