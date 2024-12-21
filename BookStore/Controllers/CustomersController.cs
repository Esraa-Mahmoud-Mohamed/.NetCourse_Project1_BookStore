using AutoMapper;
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
    public class CustomersController : ControllerBase
    {
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;
        IMapper mapper;
        UnitOfWork unitOfWork;

        public CustomersController(UserManager<IdentityUser> _userManager,RoleManager<IdentityRole> _roleManager, IMapper mapper, UnitOfWork unitOfWork)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Register(AddCustomerDTO customerDTO)
        {
            if (ModelState.IsValid)
            {
                Customer customer = mapper.Map<Customer>(customerDTO);
                IdentityResult result = userManager.CreateAsync(customer,customerDTO.Password).Result;
                if (result.Succeeded)
                {
                    IdentityResult result1 = userManager.AddToRoleAsync(customer,"customer").Result;
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

        [HttpPut]
        public IActionResult Edit(EditCustomerDTO customerDTO)
        {
            if (ModelState.IsValid)
            {
                Customer customer = (Customer)userManager.FindByIdAsync(customerDTO.Id).Result;
                customer.Name = customerDTO.Name;
                customer.Email = customerDTO.Email;
                customer.Address = customerDTO.Address;
                customer.PhoneNumber = customerDTO.PhoneNumber;
                customer.UserName = customerDTO.UserName;

                var result = userManager.UpdateAsync(customer).Result;
                if (result.Succeeded) return NoContent();
                else return BadRequest(result.Errors);
            }
            else return BadRequest(ModelState);

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = userManager.GetUsersInRoleAsync("customer").Result.OfType<Customer>().ToList();
            if (!customers.Any()) return NotFound();
            List<DisplayCustomerDTO> displayCustomerDTO = mapper.Map<List<DisplayCustomerDTO>>(customers);
            return Ok(displayCustomerDTO);
        }
        [HttpGet("{id}")]
        public IActionResult getbyid(string id)
        {

            var customer = (Customer)userManager.GetUsersInRoleAsync("customer").Result.Where(n => n.Id == id).FirstOrDefault();
            // var cu = usermanager.Users.Where(n => n.Id == id).FirstOrDefault();
            if (customer == null) return NotFound();
            DisplayCustomerDTO customerDTO = mapper.Map<DisplayCustomerDTO>(customer);

            return Ok(customerDTO);
        }

    }
}
