using System.ComponentModel.DataAnnotations;

namespace BookStore.DTOs.AdminDTOs
{
    public class AddAdminDTO
    {
        public string Name { get; set; }
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "invalid email")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
