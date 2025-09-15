using System.Text.Json.Serialization;

namespace AuthenticationApi.Application.DTOs
{
    public record LoginDto(
        [property: JsonPropertyName("email")]string Email,
        [property: JsonPropertyName("password")] string Password
        );
}