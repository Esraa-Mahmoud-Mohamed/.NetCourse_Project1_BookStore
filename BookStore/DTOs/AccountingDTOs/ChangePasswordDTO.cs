using System.ComponentModel.DataAnnotations;

namespace BookStore.DTOs.UserDTOs
{
    public class ChangePasswordDTO
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage ="Password Doesnt Match")]
        public string ConfirmPassword { get; set; }
    }
}
