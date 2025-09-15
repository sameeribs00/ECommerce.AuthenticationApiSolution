using AuthenticationApi.Application.DTOs;
using AuthenticationApi.Application.Interfaces;
using AuthenticationApi.Infrastructure.Data;
using ECommerece.CommonLibrary.Logs;
using ECommerece.CommonLibrary.Responses;
using AuthenticationApi.Application.DTOsConversion;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;


namespace AuthenticationApi.Infrastructure.Repositories
{
    public class UserRepository (AuthenticationDbContext context, IConfiguration config) : IUser
    {
        public async Task<BaseResponse> GetUserById(int userId)
        {
            try
            {
                var user = await context.User.FindAsync(userId);
                if(user is null)
                    return new BaseResponse() { IsSuccess = false, Message = $"No user was found with this Id: {userId}" };

                var userConverted = UserConversion.MapFromEntity(user);
                return new BaseResponse() { IsSuccess = true, Data = userConverted };
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new BaseResponse() { IsSuccess = false, Message = "An error occurred while retrieving the user" };
            }
        }
        public async Task<BaseResponse> GetUserByEmailAddress(string emailAddress)
        {
            try
            {
                var user = await context.User.FirstOrDefaultAsync(u => u.Email == emailAddress);
                if (user is null)
                    return new BaseResponse() { IsSuccess = false, Message = $"No user was found with this Email Address: {emailAddress}" };

                var userConverted = UserConversion.MapFromEntity(user);
                return new BaseResponse() { IsSuccess = true, Data = userConverted };
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new BaseResponse() { IsSuccess = false, Message = "An error occurred while retrieving the user" };
            }
        }

        public async Task<BaseResponse> Login(LoginDto loginRequest)
        {
            try
            {
                var user = await GetUserByEmailAddress(loginRequest.Email);
                if (user is null)
                    return new BaseResponse() { IsSuccess = false, Message = $"No user was found with this Email Address: {loginRequest.Email}" };

                var userDto = user.Data as AppUserDto;
                var verifyUser = BCrypt.Net.BCrypt.Verify(loginRequest.Password, userDto.Password);
                if(!verifyUser)
                    return new BaseResponse() { IsSuccess = false, Message = "Invalid credintials" };

                var token = GenerateToken(userDto);
                return new BaseResponse() { IsSuccess = true, Data = token };
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new BaseResponse() { IsSuccess = false, Message = "An error occurred while logging" };
            }
        }
        public async Task<BaseResponse> Register(AppUserDto user)
        {
            try
            {
                var existUser = await GetUserByEmailAddress(user.Email);
                if(existUser.IsSuccess && existUser.Data != null)
                    return new BaseResponse() { IsSuccess = false, Message = $"user with this email address: {user.Email} is already exist" };

                var result = context.User.Add(new Domain.Entities.AppUser
                {
                    Name = user.Name,
                    Role = user.Role,
                    Email = user.Email,
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber,
                    Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
                });
                await context.SaveChangesAsync();

                return result.Entity.Id > 0 ? new BaseResponse() { IsSuccess = true, Message = "User rigestered successfully"} : new BaseResponse() { IsSuccess = false, Message = "Error occurred while registering the user" };
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new BaseResponse() { IsSuccess = false, Message = "An error occurred while regitering" };
            }
        }
        private string GenerateToken(AppUserDto user)
        {
            var key = Encoding.UTF8.GetBytes(config.GetSection("Authentication:Key").Value!);
            var securityKey = new SymmetricSecurityKey(key);
            var credintials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new (ClaimTypes.Name, user.Name),
                new (ClaimTypes.Email, user.Email)
            };

            if (!string.IsNullOrEmpty(user.Role) || !Equals("string", user.Role))
                claims.Add(new(ClaimTypes.Role, user.Role));

            var token = new JwtSecurityToken(
                issuer: config["Authentication:Issuer"],
                audience: config["Authentication:Audience"],
                claims: claims,
                expires: null,
                signingCredentials: credintials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
