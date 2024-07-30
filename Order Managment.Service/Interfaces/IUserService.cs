using Order_Management.Core.Entities;
using Order_Management.Core.Repositories;
using Order_Management.Repository.Repo_Implementation;
using Order_Management.Service.Dto;
using Order_Managment.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Managment.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> RegisterAsync(UserDto userDto);
        Task<UserResponseDto> LoginAsync(UserLoginDto userLoginDto);
    }
}
