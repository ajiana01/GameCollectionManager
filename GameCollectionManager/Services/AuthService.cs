using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using GameCollectionManager.DTOs;
using GameCollectionManager.Models;
using GameCollectionManager.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace GameCollectionManager.Services;

public class AuthService(UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IConfiguration configuration,
    IMapper mapper) : IAuthService
{
    public async Task<ApiResponseDto<AuthResponseDto>> RegisterAsync(RegisterDto registerDto)
    {
        try
        {
            var existingUser = await userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return ApiResponseDto<AuthResponseDto>.ErrorResult("User with this email already exists");
            }

            var user = mapper.Map<ApplicationUser>(registerDto);
            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return ApiResponseDto<AuthResponseDto>.ErrorResult("Registration failed", errors);
            }

            await userManager.AddToRoleAsync(user, "User");

            var authResponse = await GenerateJwtToken(user);
            return ApiResponseDto<AuthResponseDto>.SuccessResult(authResponse, "Registration successful");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<AuthResponseDto>.ErrorResult($"Registration error: {ex.Message}");
        }
    }


    public async Task<ApiResponseDto<AuthResponseDto>> LoginAsync(LoginDto loginDto)
    {
        try
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return ApiResponseDto<AuthResponseDto>.ErrorResult("Invalid email or password");
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return ApiResponseDto<AuthResponseDto>.ErrorResult("Invalid email or password");
            }

            var authResponse = await GenerateJwtToken(user);
            return ApiResponseDto<AuthResponseDto>.SuccessResult(authResponse, "Login successful");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<AuthResponseDto>.ErrorResult($"Login error: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<UserDto>> GetCurrentUserAsync(string userId)
    {
        try
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return ApiResponseDto<UserDto>.ErrorResult("User not found");
            }

            var userDto = mapper.Map<UserDto>(user);
            var roles = await userManager.GetRolesAsync(user);
            userDto.Roles = roles.ToList();

            return ApiResponseDto<UserDto>.SuccessResult(userDto);
        }
        catch (Exception ex)
        {
            return ApiResponseDto<UserDto>.ErrorResult($"Error retrieving user: {ex.Message}");
        }
    }
    
    private async Task<AuthResponseDto> GenerateJwtToken(ApplicationUser user)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"] ?? "your-super-secret-key-that-is-at-least-256-bits-long");

        var roles = await userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName ?? ""),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim("FirstName", user.FirstName ?? ""),
            new Claim("LastName", user.LastName ?? "")
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        var userDto = mapper.Map<UserDto>(user);
        userDto.Roles = roles.ToList();

        return new AuthResponseDto
        {
            Token = tokenHandler.WriteToken(token),
            Expiration = tokenDescriptor.Expires.Value,
            User = userDto
        };
    }
}
