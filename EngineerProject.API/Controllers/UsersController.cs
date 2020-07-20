using EngineerProject.API.Entities;
using EngineerProject.API.Entities.Models;
using EngineerProject.API.Utility;
using EngineerProject.Commons.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace EngineerProject.API.Controllers
{
    [AllowAnonymous, ApiController, Route("api/[controller]/[action]")]
    public class UsersController : Controller
    {
        private readonly AppSettings appSettings;
        private readonly EngineerContext context;
        private readonly MessageManager messageManager;

        public UsersController(EngineerContext context, IOptions<AppSettings> appSettings, MessageManager messageManager)
        {
            this.context = context;
            this.appSettings = appSettings.Value;
            this.messageManager = messageManager;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] UserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.Email) || string.IsNullOrEmpty(userDto.Password))
                return BadRequest();

            User user;

            try
            {
                user = context.Users.SingleOrDefault(x => x.Email.Equals(userDto.Email));

                if (user == null || !VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
                    return BadRequest(new { message = "Username or password is incorrect" });
            }
            catch (Exception ex)
            {
                Logger.Log($"{nameof(UsersController)} {nameof(Authenticate)}", ex.Message, NLog.LogLevel.Warn, ex);

                return BadRequest(ex.Message);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                user.Id,
                user.Email,
                Token = tokenString
            });
        }

        [HttpGet, Authorize]
        public IActionResult GetProfile()
        {
            return Ok();
        }

        [HttpPost, Authorize]
        public IActionResult ChangePassword([FromBody] UserPasswordResetDto model)
        {
            var userId = ClaimsReader.GetUserId(Request.HttpContext.Request);

            var user = context.Users.FirstOrDefault(a => a.Id == userId);

            if (user == null)
                return NotFound();

            if (!UserValidators.CheckIfPasswordIsCorrect(model.Password))
                return BadRequest("Hasło musi posiadać wielką i małą literę, liczbę oraz znak specjalny a także składać się co najmniej z 8 znaków");

            try
            {
                if (!VerifyPasswordHash(model.OldPassword, user.PasswordHash, user.PasswordSalt))
                    return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            try
            {
                context.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult CheckPasswordRecovery([FromBody] string code)
        {
            var user = context.Users.FirstOrDefault(a => a.RecoveryCode.Equals(code));

            if (code == null || user == null)
                return BadRequest("Podano błędny kod");

            if (user.RecoveryExpirationDate.Value < DateTime.Now)
                return BadRequest("Kod jest już nieaktywny");

            return Ok();
        }

        [HttpPost]
        public IActionResult Register([FromBody] UserDto userDto)
        {
            if (!UserValidators.CheckIfEmailIsCorrect(userDto.Email))
                return BadRequest("Podaj poprawny email");

            if (string.IsNullOrWhiteSpace(userDto.Password))
                return BadRequest("Podaj poprawne hasło");

            if (context.Users.Any(x => x.Email.Equals(userDto.Email)))
                return BadRequest($"Adres email {userDto.Email} jest już zajęty");

            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Id = userDto.Id,
                Email = userDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            context.Users.Add(user);

            try
            {
                context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Log($"{nameof(UsersController)} {nameof(Register)}", ex.Message, NLog.LogLevel.Warn, ex);

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult SendPasswordRecovery([FromBody] string login)
        {
            var user = context.Users.FirstOrDefault(a => a.Email.Equals(login));

            if (user == null)
                return BadRequest("Podany użytkownik nie istnieje");

            var code = Guid.NewGuid().ToString().Replace("-", "");

            user.RecoveryCode = code;
            user.RecoveryExpirationDate = DateTime.Now.AddDays(1);

            try
            {
                context.SaveChanges();

                messageManager.SendPasswordRecoveryMessage(user.Email, user.RecoveryCode, user.RecoveryExpirationDate.Value);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult UsePasswordRecovery([FromBody] PasswordRecoveryDto model)
        {
            var user = context.Users.FirstOrDefault(a => a.Email.Equals(model.Email) && a.Email.Equals(model.Email) && a.RecoveryCode.Equals(model.Code));

            if (user == null)
                return BadRequest("Podane błędne dane");

            if (!UserValidators.CheckIfPasswordIsCorrect(model.Password))
                return BadRequest("Hasło musi posiadać wielką i małą literę, liczbę oraz znak specjalny a także składać się co najmniej z 8 znaków");

            CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.RecoveryCode = null;
            user.RecoveryExpirationDate = null;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            try
            {
                context.SaveChanges();

                Logger.Log($"{nameof(UsersController)} {nameof(UsePasswordRecovery)}", $"Hasło zostało zmienione dla użytkownika o id: {user.Id}", NLog.LogLevel.Info);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}