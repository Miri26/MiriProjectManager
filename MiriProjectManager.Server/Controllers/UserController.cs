using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiriProjectManager.Server.Data;
using MiriProjectManager.Server.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MiriProjectManager.Server.Controllers
{
    [Route("api/auth/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly LocalDBContext _context;

        public UserController(LocalDBContext context)
        {
            _context = context;
        }

        
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> RegisterUser(UserDTO userDTO)
        {
            if (!UserExists(userDTO.Username)) {
                _context.UserDTO.Add(userDTO);
                await _context.SaveChangesAsync();

                return Ok(new { id = userDTO.Id });
            }
            return Unauthorized("Username is taken.");
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> LoginUser(UserDTO userDTO)
        {
            var user = _context.UserDTO.FirstOrDefault(user => user.Username == userDTO.Username && user.Password == userDTO.Password);
            if (user == null)
            {
                return Unauthorized("The username or password is incorrect.");
            }

            var secret = "superdupersecretkeythathastobeatleast265bitslong"; // must be long enough and add to an ENV VAR
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim("name", userDTO.Username),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            string jwt = tokenHandler.WriteToken(token);

            return Ok(new { token = jwt });
        }

        private bool UserExists(string  username)
        {
            return _context.UserDTO.Any(u => u.Username == username);
        }
    }
}
