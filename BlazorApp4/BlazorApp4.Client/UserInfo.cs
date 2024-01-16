using System.Text.Json.Serialization;

namespace BlazorApp4.Client;

public record UserInfo
(
    [property: JsonPropertyName("userid")] string UserId,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("username")] string UserName,
    [property: JsonPropertyName("roles")] List<string> Roles
);
