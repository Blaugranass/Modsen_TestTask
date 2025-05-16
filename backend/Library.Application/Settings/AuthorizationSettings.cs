namespace Library.Application.Settings;

public class AuthorizationSettings
{
    public TimeSpan Expire { get; set; }

    public string SecretKey { get; set; } = string.Empty;

    public TimeSpan RefreshExpire { get; set; }
}
