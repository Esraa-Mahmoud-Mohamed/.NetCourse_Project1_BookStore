using AutoMapper;
using BookStore.DTOs.AdminDTOs;
using BookStore.DTOs.CustomerDTOs;
using BookStore.Models;
using BookStore.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;
        IMapper mapper;

        public AdminsController(UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager, IMapper mapper)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Register(AddAdminDTO adminDTO)
        {
            if (ModelState.IsValid)
            {
                Admin admin = mapper.Map<Admin>(adminDTO);
                IdentityResult result = userManager.CreateAsync(admin, adminDTO.Password).Result;
                if (result.Succeeded)
                {
                    IdentityResult result1 = userManager.AddToRoleAsync(admin, "admin").Result;
                    if (result1.Succeeded) return Ok();
                    else return BadRequest(result1.Errors);
                }
                else return BadRequest(result.Errors);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        
    }
}
