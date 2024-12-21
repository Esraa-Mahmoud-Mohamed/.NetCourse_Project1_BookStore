using AutoMapper;
using BookStore.DTOs.AccountingDTOs;
using BookStore.DTOs.UserDTOs;
using BookStore.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountingsController : ControllerBase
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;

        public AccountingsController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> signInManager)
        {
            userManager = _userManager;
            this.signInManager = signInManager;
        }

        [HttpPut]
        public IActionResult ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = userManager.FindByIdAsync(changePasswordDTO.Id).Result;
                var result = userManager.ChangePasswordAsync(user, changePasswordDTO.OldPassword, changePasswordDTO.NewPassword).Result;
                if (result.Succeeded) return Ok();
                else return BadRequest(result.Errors);
            }
            else return BadRequest(ModelState);
        }
        [HttpPost]
        public IActionResult login(LoginDTO loginDTO)
        {
            var result = signInManager.PasswordSignInAsync(loginDTO.UserName, loginDTO.Password, false, false).Result;
            if (result.Succeeded)
            {
                var user = userManager.FindByNameAsync(loginDTO.UserName).Result;

                List<Claim> userdata = new List<Claim>();
                userdata.Add(new Claim(ClaimTypes.Name, user.UserName));
                userdata.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

                var roles = userManager.GetRolesAsync(user).Result;
                foreach (var itemRole in roles)
                {
                    userdata.Add(new Claim(ClaimTypes.Role, itemRole));
                }

                string key = "A_Secure_256_Bit_Key_That_Is_32_Bytes_Long";
                var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

                var signingcer = new SigningCredentials(secertkey, SecurityAlgorithms.HmacSha256);
                
                var token = new JwtSecurityToken(
                     claims: userdata,
                     expires: DateTime.Now.AddDays(2),
                     signingCredentials: signingcer
                     );
                var tokenstring = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(tokenstring);
            }
            else return Unauthorized("invalid username or pasword");
        }
        
        [HttpGet("logout")]
        [Authorize]
        public IActionResult logout()
        {
            signInManager.SignOutAsync();
            return Ok();
        }
    }
}
