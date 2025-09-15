using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuthenticationApi.Application.DTOs
{
    public record AppUserDto(
        [property: JsonPropertyName("id")] int Id,
        [Required][property: JsonPropertyName("name")] string? Name,
        [Required][property: JsonPropertyName("email")] string? Email, 
        [Required][property: JsonPropertyName("phoneNumber")] string? PhoneNumber, 
        [Required][property: JsonPropertyName("address")] string? Address, 
        [Required][property: JsonPropertyName("password")] string? Password, 
        [Required][property: JsonPropertyName("role")] string? Role
        );
}
