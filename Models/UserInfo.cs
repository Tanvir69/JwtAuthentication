using System.ComponentModel.DataAnnotations;

namespace JWTAUthentication.Models;


    public class UserInfo
    {
        [Required(ErrorMessage = "User Id is required")]
         public int UserId { get; set; }

        [Required(ErrorMessage = "Display is required")]
        public string? DisplayName { get; set; }

        [Required(ErrorMessage = "User name is required")]
         public string? UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "User name is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "PasswordUser name is required")]
        public string? Password { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
