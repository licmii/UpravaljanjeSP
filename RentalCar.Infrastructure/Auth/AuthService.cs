using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RentalCar.Application.Common.Dto.Auth;
using RentalCar.Application.common.Interfaces;
using RentalCar.Infrastructure.Configuration;

namespace RentalCar.Infrastructure.Auth;

public class AuthService : IAuthService
{

    private readonly ApplicationUserManager _userManager;
    private readonly JwtConfiguration _jwtConfiguration;
    private const string Purpose = "passwordless-auth";
    private const string Provider = "PasswordlessLoginTokenProvider";

    public AuthService(ApplicationUserManager userManager, IOptions<JwtConfiguration> jwtConfiguration)
    {
        _userManager = userManager;
        _jwtConfiguration = jwtConfiguration.Value;
    }

    public async Task<BeginLoginResponseDto> BeginLoginAsync(string emailAddress)
    {

        var user = await _userManager.FindByEmailAsync(emailAddress);
        string? validationToken = null;

        if (user != null)
        {
            var token = await _userManager.GenerateUserTokenAsync(user,
                Provider,
                Purpose);
            var bytes = Encoding.UTF8.GetBytes($"{token}:{emailAddress}");
            validationToken = Convert.ToBase64String(bytes);
        }
        
        //todo :: send email with this validation token
        return new BeginLoginResponseDto(validationToken);
    }

    public async Task<CompleteLoginResponseDto> CompleteLoginAsync(string token)
    {
        var (userToken, emailAddress) = ExtractValidationToken(token);
        var user = await _userManager.FindByEmailAsync(emailAddress);

        if (user is not null)
        {
            var isValid = await _userManager.VerifyUserTokenAsync(user,
                Provider,
                Purpose,
                userToken);

            if (isValid)
            {
                await _userManager.UpdateSecurityStampAsync(user); 

                var authClaims = new List<Claim>();
                var roles = new List<string>();

                var rolesFromDb = await _userManager.GetRolesAsync(user);

                foreach (var roleFromDb in rolesFromDb)
                {
                    roles.Add(roleFromDb);
                    authClaims.Add(new Claim(ClaimTypes.Role, roleFromDb));
                }

                foreach (var item in user.Claims)
                {
                    authClaims.Add(new Claim(item.Type, item.Value));
                }

                return new CompleteLoginResponseDto(user.Email, roles,
                    new JwtSecurityTokenHandler().WriteToken(GenerateJwtToken(authClaims)));

            }
            
        }
        
        
        throw new NotImplementedException();
    }

    private static Tuple<string, string> ExtractValidationToken(string token)
    {
        var base64EncodedBytes = Convert.FromBase64String(token);
        var tokenDetails = Encoding.UTF8.GetString(base64EncodedBytes);
        var separatorIndex = tokenDetails.IndexOf(':');

        return new Tuple<string, string>(tokenDetails[..separatorIndex], tokenDetails[(separatorIndex + 1)..]);
    }

    private JwtSecurityToken GenerateJwtToken(IEnumerable<Claim> authClaims)
    {
        var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Secret!));

        var token = new JwtSecurityToken(
                issuer: _jwtConfiguration.ValidIssuer,
                audience: _jwtConfiguration.ValidAudience,
                expires: DateTime.Now.AddMinutes(15),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }
    
}