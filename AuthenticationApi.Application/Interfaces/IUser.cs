using AuthenticationApi.Application.DTOs;
using ECommerece.CommonLibrary.Responses;

namespace AuthenticationApi.Application.Interfaces
{
    public interface IUser
    {
        Task<BaseResponse> Register(AppUserDto user);
        Task<BaseResponse> Login(LoginDto loginRequest);
        Task<BaseResponse> GetUserById(int userId);
        Task<BaseResponse> GetUserByEmailAddress(string eamilAddress);
    }
}