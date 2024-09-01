using BusinessManagementSystem.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessManagementSystem.Services
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, LoginResponseDto user);
        bool ValidateToken(string key, string issuer, string token);
    }
}
