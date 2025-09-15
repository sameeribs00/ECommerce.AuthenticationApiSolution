using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationApi.Application.DTOs;
using AuthenticationApi.Domain.Entities;

namespace AuthenticationApi.Application.DTOsConversion
{
    public class UserConversion
    {
        public static AppUser MapIntoEntity(AppUserDto userDto) => new AppUser()
        {
            Id = userDto.Id,
            Name = userDto.Name,
            Email = userDto.Email,
            PhoneNumber = userDto.PhoneNumber,
            Address = userDto.Address,
            Password = userDto.Password,
            Role = userDto.Role
        };
        public static AppUserDto MapFromEntity(AppUser user) => new(user.Id, user.Name, user.Email, user.PhoneNumber, user.Address, user.Password, user.Role);
        public static IEnumerable<AppUserDto>? MapFromEntity(IEnumerable<AppUser>? users) => users?.Select(MapFromEntity).ToList();
    }
}
